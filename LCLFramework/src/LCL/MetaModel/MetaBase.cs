using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace LCL.MetaModel
{
    /// <summary>
    /// 职责与支持：
    /// SetFieldValue
    /// ICustomParamsHolder
    /// INotifyPropertyChanged（由于有时元数据直接需要被绑定到 WPF 界面中，所以需要实现这个接口）
    /// </summary>
    public abstract class MetaBase : Freezable, INotifyPropertyChanged
    {
        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            this.CheckUnFrozen();

            if (propertyName != null)
            {
                if (!object.Equals(field, value))
                {
                    field = value;

                    this.NotifyPropertyChanged(propertyName);
                }
            }
            else
            {
                field = value;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var hander = this.PropertyChanged;
            if (hander != null) hander(this, e);
        }

        #endregion
    }
    /// <summary>
    /// ready for extend
    /// </summary>
    public class FreezableCloneOptions
    {
        /// <summary>
        /// 此数据用于防止循环引用对象时，进行重复的拷贝而导航溢出。
        /// </summary>
        internal Dictionary<Freezable, Freezable> CopiedPairs = new Dictionary<Freezable, Freezable>();

        public bool DeepCloneRef { get; set; }

        public bool CloneChildren { get; set; }
    }
}
