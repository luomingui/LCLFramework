using LCL.ComponentModel;

namespace LCL.ObjectContainers.Unity
{

    // 我们项目中什么时候要使用到Unity呢，如下情况：
    //1.所构建的系统依赖于健全的面向对象原则，但是大量不同的代码交织在一起而难以维护。
    //2.构建的对象和类需要依赖其他对象或类。
    //3.依赖于复杂的或需要抽象的对象。
    //4.希望利用构造函数、方法或属性的调用注入优势。
    //5.希望管理对象实例的生命周期。
    //6.希望能够在运行时管理并改变依赖关系。
    //7.希望在拦截方法或属性调用的时候生成一个策略链或管道处理容器来实现横切（AOP）任务。
    //8.希望在Web Application中的回发操作时能够缓存或持久化依赖关系。

    /// <summary>
    /// 从 Unity 适配到 IObjectContainer 的插件。
    /// 使用此插件后，LCL 平台的 IOC 框架将由 UnityContainer 来实现。
    /// </summary>
    public class UnityAdapterPlugin : LCLPlugin
    {
        protected override int SetupLevel
        {
            get { return PluginSetupLevel.System; }
        }
        public override void Initialize(IApp app)
        {
            LEnvironment.AppObjectContainer = new UnityObjectContainer();
        }
    }
}
