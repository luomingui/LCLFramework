
#region Namespaces
using System;
using System.Threading;
using System.Collections;
#endregion

namespace UIShell.Threading
{
    /// <summary>
    /// 
    /// </summary>
	public class Semaphore
	{
		#region Member Variables
		private int _count;
		private object _semLock = new object();
		#endregion

		#region Construction
		
		public Semaphore() : this(1) 
		{
		}
		public Semaphore(int count) 
		{
			if (count < 0) throw new ArgumentException("Semaphore must have a count of at least 0.", "count");
			_count = count;
		}
		#endregion

		#region Synchronization Operations
		public void AddOne() { V(); }
		public void WaitOne() { P(); }
		public void P() 
		{
			lock(_semLock) 
			{
                while (_count <= 0) Monitor.Wait(_semLock, Timeout.Infinite);
				_count--;
			}
		}
		public void V() 
		{
			lock(_semLock) 
			{
				_count++;
		    	Monitor.Pulse(_semLock);
       	    }
		}

		public void Reset(int count)
		{
			lock(_semLock) { _count = count; }
		}
		#endregion
	}
    /// <summary>
    /// 
    /// </summary>
	public class ManagedThreadPool
	{
		#region Constants
		private const int _maxWorkerThreads = 10;
		#endregion

		#region Member Variables
		/// <summary>Queue of all the callbacks waiting to be executed.</summary>
		private static Queue _waitingCallbacks;
		/// <summary>
		/// Used to signal that a worker thread is needed for processing.  Note that multiple
		/// threads may be needed simultaneously and as such we use a semaphore instead of
		/// an auto reset event.
		/// </summary>
		private static Semaphore _workerThreadNeeded;
		/// <summary>List of all worker threads at the disposal of the thread pool.</summary>
		private static ArrayList _workerThreads;
		/// <summary>Number of threads currently active.</summary>
		private static int _inUseThreads;
		/// <summary>Lockable object for the pool.</summary>
		private static object _poolLock = new object();
		#endregion

		#region Construction and Finalization
		/// <summary>Initialize the thread pool.</summary>
		static ManagedThreadPool() { Initialize(); }

		/// <summary>Initializes the thread pool.</summary>
		private static void Initialize()
		{
            
			// Create our thread stores; we handle synchronization ourself
			// as we may run into situtations where multiple operations need to be atomic.
			// We keep track of the threads we've created just for good measure; not actually
			// needed for any core functionality.
			_waitingCallbacks = new Queue();
			_workerThreads = new ArrayList();
			_inUseThreads = 0;

			// Create our "thread needed" event
			_workerThreadNeeded = new Semaphore(0);
			
			// Create all of the worker threads
			for(int i=0; i<_maxWorkerThreads; i++)
			{
				// Create a new thread and add it to the list of threads.
				Thread newThread = new Thread(new ThreadStart(ProcessQueuedItems));
				_workerThreads.Add(newThread);

				// Configure the new thread and start it
				newThread.Name = "ManagedPoolThread #" + i.ToString();
				newThread.IsBackground = true;
				newThread.Start();
			}
		}
		#endregion

		#region Public Methods
		/// <summary>Queues a user work item to the thread pool.</summary>
		/// <param name="callback">
		/// A WaitCallback representing the delegate to invoke when the thread in the 
		/// thread pool picks up the work item.
		/// </param>
		public static void QueueUserWorkItem(WaitCallback callback)
		{
			// Queue the delegate with no state
			QueueUserWorkItem(callback, null);
		}

		/// <summary>Queues a user work item to the thread pool.</summary>
		/// <param name="callback">
		/// A WaitCallback representing the delegate to invoke when the thread in the 
		/// thread pool picks up the work item.
		/// </param>
		/// <param name="state">
		/// The object that is passed to the delegate when serviced from the thread pool.
		/// </param>
		public static void QueueUserWorkItem(WaitCallback callback, object state)
		{
    		// Create a waiting callback that contains the delegate and its state.
			// At it to the processing queue, and signal that data is waiting.
			WaitingCallback waiting = new WaitingCallback(callback, state);
			lock(_poolLock) { _waitingCallbacks.Enqueue(waiting); }
			_workerThreadNeeded.AddOne();
		}

		/// <summary>Empties the work queue of any queued work items.  Resets all threads in the pool.</summary>
		public static void Reset()
		{
			lock(_poolLock) 
			{ 
				// Cleanup any waiting callbacks
				try 
				{
					// Try to dispose of all remaining state
					foreach(object obj in _waitingCallbacks)
					{
						WaitingCallback callback = (WaitingCallback)obj;
						if (callback.State is IDisposable) ((IDisposable)callback.State).Dispose();
					}
				} 
				catch{}

				// Shutdown all existing threads
				try 
				{
					foreach(Thread thread in _workerThreads) 
					{
						if (thread != null) thread.Abort("reset");
					}
				}
				catch{}

				// Reinitialize the pool (create new threads, etc.)
				Initialize();
			}
		}
		#endregion

		#region Properties
		/// <summary>Gets the number of threads at the disposal of the thread pool.</summary>
		public static int MaxThreads { get { return _maxWorkerThreads; } }
		/// <summary>Gets the number of currently active threads in the thread pool.</summary>
		public static int ActiveThreads { get { return _inUseThreads; } }
		/// <summary>Gets the number of callback delegates currently waiting in the thread pool.</summary>
		public static int WaitingCallbacks { get { lock(_poolLock) { return _waitingCallbacks.Count; } } }
		#endregion

		#region Thread Processing
		/// <summary>Event raised when there is an exception on a threadpool thread.</summary>
		/// <summary>A thread worker function that processes items from the work queue.</summary>
		private static void ProcessQueuedItems()
		{
			// Process indefinitely
			while(true)
			{
                //判断有无资源操作，如果有则执行【即从队列中取】，如没有则将当前线程置为等待。注意当前信息量中count数量与_waitingCallbacks队列长度相同。
                //通过QueueUserWorkItem的_workerThreadNeeded.AddOne();代码行来告之在当前线程池中处于wait的线程列表中找出一个线程来执行_waitingCallbacks列队中的任务（Dequeue）.
                //如果进程池中所有线程均为执行状态时，则系统还运行QueueUserWorkItem（）函数则可视其仅将任务放入队列即可，因为此处的while(true)会不断从队列中获取任务执行。
				_workerThreadNeeded.WaitOne();

				// Get the next item in the queue.  If there is nothing there, go to sleep
				// for a while until we're woken up when a callback is waiting.
				WaitingCallback callback = null;

				// Try to get the next callback available.  We need to lock on the 
				// queue in order to make our count check and retrieval atomic.
				lock(_poolLock)
				{
					if (_waitingCallbacks.Count > 0)
					{
						try { callback = (WaitingCallback)_waitingCallbacks.Dequeue(); } 
						catch{} // make sure not to fail here
					}
				}

				if (callback != null)
				{
					// We now have a callback.  Execute it.  Make sure to accurately
					// record how many callbacks are currently executing.
					try 
					{
						Interlocked.Increment(ref _inUseThreads);
						callback.Callback(callback.State);
                    }
                    catch
                    {
                        ;
                    }
					finally
					{
						Interlocked.Decrement(ref _inUseThreads);
					}
				}
			}
		}
		#endregion

        /// <summary>
        /// 
        /// </summary>
		private class WaitingCallback
		{
			#region Member Variables
			/// <summary>Callback delegate for the callback.</summary>
			private WaitCallback _callback;
			/// <summary>State with which to call the callback delegate.</summary>
			private object _state;
			#endregion

			#region Construction
			/// <summary>Initialize the callback holding object.</summary>
			/// <param name="callback">Callback delegate for the callback.</param>
			/// <param name="state">State with which to call the callback delegate.</param>
			public WaitingCallback(WaitCallback callback, object state)
			{
				_callback = callback;
				_state = state;
			}
			#endregion

			#region Properties
			/// <summary>Gets the callback delegate for the callback.</summary>
			public WaitCallback Callback { get { return _callback; } }
			/// <summary>Gets the state with which to call the callback delegate.</summary>
			public object State { get { return _state; } }
			#endregion
		}
	}
}
