using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LCL.Threading
{
    /// <summary>
    /// 提供一个队列的线程处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueServer<T> : IDisposable
    {
        private Thread thread = null;
        private Queue<T> queue = new Queue<T>();
        private bool isBackground = false;

        public QueueServer()
        {
        }
        public void Dispose()
        {
            try
            {

                this.ClearItems();

            }
            finally
            {

            }
        }

        #region  公共方法

        public void EnqueueItem(T item)
        {
            lock (this.queue)
            {
                this.queue.Enqueue(item);
            }
            if ((this.thread == null) || !(this.thread.IsAlive))
            {
                this.CreateThread();
                this.thread.Start();
            }
        }

        public void ClearItems()
        {
            lock (this.queue)
            {
                this.queue.Clear();
            }
        }

        #endregion

        #region 线程处理

        private void CreateThread()
        {
            this.thread = new Thread(new ThreadStart(this.ThreadProc));
            this.thread.IsBackground = this.isBackground;
        }

        private void ThreadProc()
        {
            T item = default(T);
            while (true)
            {
                lock (this.queue)
                {
                    if (this.queue.Count > 0)
                    {
                        item = this.queue.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }
                try
                {
                    this.OnProcessItem(item);
                }
                catch
                {
                }
            }
        }

        protected virtual void OnProcessItem(T item)
        {
            if (ProcessItem != null)
            {
                ProcessItem(item);
            }
        }

        public event Action<T> ProcessItem;

        #endregion

        #region  属性

        public bool IsBackground
        {
            get
            {
                return this.isBackground;
            }
            set
            {
                this.isBackground = true;
                if ((this.thread != null) && (this.thread.IsAlive))
                {
                    this.thread.IsBackground = this.isBackground;
                }
            }
        }

        public T[] Items
        {
            get
            {
                lock (this.queue)
                {
                    return this.queue.ToArray();
                }
            }
        }

        public int QueueCount
        {
            get
            {
                lock (this.queue)
                {
                    return this.queue.Count;
                }
            }
        }

        #endregion


    }
}
