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
using System.Threading;

namespace SF.Threading
{
    /// <summary>
    /// 异步调用器。
    /// </summary>
    public class AsyncWorker
    {
        private Action _action;

        private EventWaitHandle _signal = new ManualResetEvent(false);

        /// <summary>
        /// 异步调用某个任务。
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (this._action != null) throw new InvalidOperationException("Previous action is running!");

            this._action = action;
            this._signal.Reset();

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    this._action();
                }
                finally
                {
                    this._signal.Set();
                    this._action = null;
                }
            });

        }

        /// <summary>
        /// 等待上次调用的任务执行完毕。
        /// </summary>
        public void WaitEnding( )
        {
            this._signal.WaitOne();
        }

        /// <summary>
        /// 安全地对任务进行异步调用。
        /// 
        /// 原因：在异步线程中调用任务，如果出现异常，往往会使整个应用程序死机。
        /// </summary>
        /// <param name="action"></param>
        public static void SafeInvoke(Action action)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    action();
                }
                catch { }
            });
        }
    }
}
