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
    public partial class AggregateRoot : System.MarshalByRefObject, IAggregateRoot
    {
        #region 构造函数及工厂方法
        protected AggregateRoot()
        {
            ID = Guid.NewGuid();
        }
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static AggregateRoot New(Type entityType)
        {
            return Activator.CreateInstance(entityType, true) as AggregateRoot;
        }
        #endregion
        public Guid ID { set; get; }
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
            if (obj == null || !(obj is AggregateRoot))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            AggregateRoot item = (AggregateRoot)obj;

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
        public static bool operator ==(AggregateRoot left, AggregateRoot right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
        public static bool operator !=(AggregateRoot left, AggregateRoot right)
        {
            return !(left == right);
        }
        #endregion
    }
}
