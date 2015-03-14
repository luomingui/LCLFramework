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
using System.Diagnostics;
using System.Threading;

namespace SF.Threading
{
    #region 异步加载数据的关系

    //[NonSerialized]
    //private ForeAsyncLoader _relationLoader;

    ///// <summary>
    ///// 如果是树，则异步整理数据。
    ///// 
    ///// 由于本类的操作都要用到树的关系，但是建立这个关系需要一定时间，所以这里采用异步模式。
    ///// </summary>
    //public ForeAsyncLoader RelationLoader
    //{
    //    get
    //    {
    //        if (this._relationLoader == null)
    //        {
    //            this._relationLoader = new ForeAsyncLoader(this.LoadRelation);
    //        }
    //        return this._relationLoader;
    //    }
    //}

    //private void LoadRelation()
    //{
    //    var treeList = this._displayList as IOrderedTreeNodeCollection;
    //    if (treeList != null)
    //    {
    //        treeList.EnsureObjectRelations();
    //    }
    //}

    #endregion

    /// <summary>
    /// 预加载的实现类。
    /// 
    /// 实现某个action的预加载。
    /// </summary>
    public class ForeAsyncLoader
    {
        /// <summary>
        /// 同步两个线程的信号
        /// </summary>
        private EventWaitHandle _signal = new ManualResetEvent(false);

        /// <summary>
        /// 表示是否已经运行过。
        /// </summary>
        private bool _runOver;

        /// <summary>
        /// 真正执行的耗时的操作
        /// </summary>
        private Action _action;

        /// <summary>
        /// 构造一个对应指定方法的预加载器。
        /// </summary>
        /// <param name="loadAction">
        /// 真正的加载方法，比较耗时的操作。
        /// </param>
        public ForeAsyncLoader(Action loadAction)
        {
            Debug.Assert(loadAction != null, "loadAction != null");
            this._action = loadAction;
        }

        public event EventHandler ActionEnded;

        /// <summary>
        /// 开始异步进行预加载。
        /// 
        /// 如果在进行执行Reset操作前，调用本方法多次，也只会执行一次loadAction。
        /// </summary>
        public void BeginLoading( )
        {
            if (!this._runOver)
            {
                this._runOver = true;

                //在后台线程中，执行这个操作
                ThreadPool.QueueUserWorkItem(delegate
                {
                    bool runSuccessful = false;
                    try
                    {
                        this._action();
                        runSuccessful = true;
                    }
                    finally
                    {
                        this._signal.Set();
                    }

                    if (runSuccessful)
                    {
                        if (this.ActionEnded != null)
                        {
                            this.ActionEnded(this, EventArgs.Empty);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 等待异步加载完成。
        /// 
        /// （注意，如果在这个方法之前没有调用Begin，则这里也会先调用Begin。）
        /// </summary>
        public void WaitForLoading( )
        {
            this.BeginLoading();
            this._signal.WaitOne();
        }

        /// <summary>
        /// 重设本加载器，使得BeginLoading可以再次起作用。
        /// </summary>
        public void Reset( )
        {
            this._runOver = false;
            this._signal.Reset();
        }
    }
}
