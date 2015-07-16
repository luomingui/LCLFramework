using LCL.DataPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Repositories
{
    public abstract partial class Repository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// 是否声明本仓库为本地仓库（客户端只在客户端查询，服务端在服务端查询）
        /// </summary>
        public DataPortalLocation DataPortalLocation { get; protected set; }

        #region 获取对象接口方法
        protected TEntityList FetchListCast<TEntityList>(object criteria, string methodName)
            where TEntityList : class, IEnumerable<TEntity>
        {
            return this.FetchList(criteria, methodName) as TEntityList;
        }
        protected TEntity FetchFirstAs(object criteria, string methodName)
        {
            return this.FetchFirst(criteria, methodName) as TEntity;
        }
        protected IEnumerable<TEntity> FetchList(object criteria, string methodName)
        {
            var list = DataPortalApi.Action(this.GetType(), methodName, criteria, this.DataPortalLocation) as IEnumerable<TEntity>;
            return list;
        }
        protected TEntity FetchFirst(object criteria, string methodName)
        {
            var list = this.FetchList(criteria, methodName);

            var last = list.DefaultIfEmpty<TEntity>() as TEntity;

            return list.Count() > 0 ? last : null;
        }
        #endregion
    }
}
