using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Caching
{
    /// <summary>
    /// 用于缓存是否过期的实时检测器
    /// </summary>
    public abstract class ChangeChecker
    {
        /// <summary>
        /// 是否已经发生了更改。
        /// </summary>
        public bool HasChanged { get; private set; }

        /// <summary>
        /// 检测是否发生更改。
        /// </summary>
        public abstract void Check();

        /// <summary>
        /// 子类调用方法通知发生了更改。
        /// </summary>
        protected void NotifyChanged()
        {
            this.HasChanged = true;
        }

        /// <summary>
        /// 检测器需要实现Memoto模式，可能需要保存起来。
        /// </summary>
        /// <returns></returns>
        public virtual CheckerMemoto GetMemoto()
        {
            throw new NotSupportedException("本类不支持持久化。");
        }
    }

    /// <summary>
    /// ChangeChecker的可序列化的Memoto。
    /// </summary>
    [Serializable]
    public abstract class CheckerMemoto
    {
        /// <summary>
        /// Memoto可以还原原有的ChangeChecker。
        /// </summary>
        /// <returns></returns>
        public abstract ChangeChecker Restore();
    }

    public class TimeChecker : ChangeChecker
    {
        
        public TimeChecker(Type regionType, Type scopeClass = null, string scopeId = null)
        {

        }
        public override void Check()
        {
            throw new NotImplementedException();
        }
    }
}
