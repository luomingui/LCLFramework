using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace LCL.Reflection
{
    /// <summary>
    /// 一个简单的 Emit 上下文。
    /// 目前只是提供了一个基本的 ModuleBuilder。
    /// </summary>
    public class EmitContext
    {
        public static readonly EmitContext Instance = new EmitContext();

        private EmitContext( ) { }

        private ModuleBuilder _module;

        private object _moduleLock = new object();

        /// <summary>
        /// 获取动态的模块，所有的类都生成在这个模块中。
        /// </summary>
        /// <returns></returns>
        public ModuleBuilder GetDynamicModule( )
        {
            if (this._module == null)
            {
                lock (this._moduleLock)
                {
                    if (this._module == null)
                    {
                        AppDomain myDomain = Thread.GetDomain();
                        var myAsmName = new AssemblyName("EmitContext_DynamicAssembly");
                        var myAsmBuilder = myDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run);
                        this._module = myAsmBuilder.DefineDynamicModule("EmitContext_DynamicModule");
                    }
                }
            }

            return this._module;
        }
    }
}
