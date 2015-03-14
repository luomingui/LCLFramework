using System;

namespace LCL.MetaModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
    public class DisplayAttribute : Attribute
    {
        public string Label { get; set; }
        public string FieldName { get; set; }
        public bool Visible { get; set; }
        public int Order { get; set; }
        public string GroupName { get; set; }
        public DisplayAttribute()
        {
            Visible = true;
        }
    }
}
