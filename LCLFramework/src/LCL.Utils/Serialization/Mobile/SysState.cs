
using LCL.Serialization.Mobile;

namespace LCL
{
    /// <summary>
    /// 系统中 PrimitiveType 在使用 Mobile 序列化时，需要使用这个对象来进行封装。
    /// 此文件夹中的类主要用于实现 JSON 自定义序列化。
    /// </summary>
    internal class SysState : MobileObject
    {
        public object Value;
        public string TypeName;

        protected override void OnMobileSerializeState(ISerializationContext context)
        {
            base.OnMobileSerializeState(context);

            context.AddState("v", this.Value);
            context.AddState("t", this.TypeName);
        }

        protected override void OnMobileDeserializeState(ISerializationContext context)
        {
            this.Value = context.GetState<object>("v");
            this.TypeName = context.GetState<string>("t");

            base.OnMobileDeserializeState(context);
        }
    }
}
