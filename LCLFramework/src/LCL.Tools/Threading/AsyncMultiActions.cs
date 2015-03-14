/*******************************************************
 * 
 * 作者：罗敏贵
 * 创建时间：2012612
 * 说明：此文件只包含一个类，具体内容见类型注释。
 * 运行环境：.NET 2.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 罗敏贵 2012612
 * 
*******************************************************/
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF.Threading
{
    /// <summary>
    /// 任意开始，一起结束
    /// </summary>
    public interface IAsyncMultiActions
    {
        /// <summary>
        /// 最后一个异步任务执行完毕后
        /// </summary>
        event EventHandler LastActionEnded;
        /// <summary>
        /// 第一个异步任务开始执行时
        /// </summary>
        event EventHandler FirstActionStarted;

        /// <summary>
        /// 调用此方法时，任务直接进入调度队列中
        /// </summary>
        /// <param name="action"></param>
        void Execute(Action action);
    }
    internal class AsyncMultiActions : IAsyncMultiActions
    {
        public static readonly AsyncMultiActions Instance = new AsyncMultiActions();

        private Queue<AutoResetEvent> _events = new Queue<AutoResetEvent>();
        private bool _started;

        private AsyncMultiActions( ) { }

        public event EventHandler FirstActionStarted;
        public event EventHandler LastActionEnded;


        public void Execute(Action action)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            lock (this)
            {
                this._events.Enqueue(autoResetEvent);
            }

            //在后台线程中，执行这个操作
            ThreadPool.QueueUserWorkItem(delegate
            {
                //执行Action
                action();

                autoResetEvent.Set();
            });

            this.Start();
        }

        private void Start( )
        {
            bool raiseStarted = false;
            lock (this)
            {
                if (this._started == false)
                {
                    this._started = true;
                    raiseStarted = true;

                    this.CreateEnder();
                }
            }
            if (raiseStarted)
            {
                if (this.FirstActionStarted != null)
                {
                    this.FirstActionStarted(this, EventArgs.Empty);
                }
            }
        }
        private void CreateEnder( )
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                while (true)
                {
                    int count = 0;
                    lock (this)
                    {
                        count = this._events.Count;
                    }
                    if (count <= 0)
                    {
                        break;
                    }

                    AutoResetEvent autoResetEvent = null;
                    lock (this)
                    {
                        autoResetEvent = this._events.Dequeue();
                    }
                    using (autoResetEvent)
                    {
                        autoResetEvent.WaitOne();
                    }
                }

                this.End();
            });
        }
        private void End( )
        {
            bool raise = false;
            lock (this)
            {
                if (this._started)
                {
                    this._started = false;
                    raise = true;
                }
            }
            if (raise)
            {
                if (this.LastActionEnded != null)
                {
                    this.LastActionEnded(this, EventArgs.Empty);
                }
            }
        }
    }
}
