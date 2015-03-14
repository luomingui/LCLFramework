using System;

namespace LCL.MetaModel.Attributes
{
    /// <summary>
    /// 所有实体对象都应该标记这个属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class EntityAttribute : Attribute { }
}
