using System;
using System.Collections;
using System.Collections.Generic;

namespace LCL.MetaModel
{
    public class MetaRepositoryBase<TMeta> : IEnumerable<TMeta>
     where TMeta : MetaBase
    {
        private List<Action> _relationSetters = new List<Action>(100);

        private List<TMeta> _allPrimes = new List<TMeta>();

        protected void AddPrime(TMeta meta)
        {
            lock (this._allPrimes) this._allPrimes.Add(meta);
        }

        /// <summary>
        /// 把某个行为添加到“待办”列表中，
        /// 等待所有的元数据都准备好后再执行。
        /// 
        /// 一般用于关系属性的设置
        /// </summary>
        /// <param name="a"></param>
        protected void FireAfterAllPrimesReady(Action a)
        {
            lock (this._relationSetters) this._relationSetters.Add(a);
        }

        /// <summary>
        /// 在完成添加初始的单个元数据之后，进行元数据间的关系设置。
        /// </summary>
        internal void InitRelations( )
        {
            if (this._relationSetters == null)
                throw new InvalidOperationException("关系已经被初始化。");

            foreach (var relation in this._relationSetters) { relation(); }

            this._relationSetters = null;
        }

        /// <summary>
        /// 冻结所有的命令元数据
        /// </summary>
        internal void Freeze( )
        {
            foreach (var v in this) v.Freeze();
        }

        internal List<TMeta> GetInnerList( )
        {
            return this._allPrimes;
        }

        #region IEnumerable<TMeta>

        IEnumerator<TMeta> IEnumerable<TMeta>.GetEnumerator( )
        {
            return this._allPrimes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._allPrimes.GetEnumerator();
        }

        #endregion
    }
}
