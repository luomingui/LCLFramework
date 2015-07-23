using LCL.Repositories;
using LCL.Serialization.Mobile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LCL
{
    [Serializable]
    public partial class Entity : System.MarshalByRefObject, IEntity
    {

        #region 构造函数及工厂方法
        protected Entity()
        {
            ID = Guid.NewGuid();
            AddDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            IsDelete = false;
        }
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Entity New(Type entityType)
        {
            return Activator.CreateInstance(entityType, true) as Entity;
        }
        #endregion

        #region IEntity
        public Guid ID { set; get; }
        public bool IsDelete { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        #endregion
        public IRepository<IEntity> Repo
        {
            get
            {
                return RF.Find(this.GetType()) as IRepository<IEntity>;
            }
        }
        internal void SaveRoot()
        {
            throw new NotImplementedException();
        }
        public bool IsTransient()
        {
            return this.ID == Guid.Empty;
        }

        #region Override Methods
        int? _requestedHashCode;
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.ID == this.ID;
        }
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.ID.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
        #endregion
    }
}
