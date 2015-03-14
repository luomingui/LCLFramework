
using System.Diagnostics;

namespace LCL.MetaModel
{
    /// <summary>
    /// 元数据
    /// </summary>
    [DebuggerDisplay("Name:{Name},Label:{Label}")]
    public class Meta : MetaBase
    {
        private string _name;
        /// <summary>
        /// 名字
        /// </summary>
        public virtual string Name
        {
            get { return this._name; }
            set { this.SetValue(ref this._name, value); }
        }
    }
    /// <summary>
    /// 视图元数据
    /// </summary>
    [DebuggerDisplay("Name:{Name} Label:{Label}")]
    public class ViewMeta : Meta
    {
        private string _label;
        /// <summary>
        /// 显示的标题
        /// </summary>
        public virtual string Label
        {
            get { return this._label; }
            set { this.SetValue(ref this._label, value, "Label"); }
        }

        private bool _IsVisible = true;
        public virtual bool IsVisible
        {
            get { return this._IsVisible; }
            set { this.SetValue(ref this._IsVisible, value); }
        }
    }
}
