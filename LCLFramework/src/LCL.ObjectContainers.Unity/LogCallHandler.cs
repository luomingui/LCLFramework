using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LCL.ObjectContainers.Unity
{
    /// <summary>
    /// [LogTimeAttributes (Order = 1,Msg = "查询单个商品信息" )]
    /// </summary>
    public class LogTimeAttributes : HandlerAttribute
   {
        public int Order { get; set; }
        public string Msg { get; set; }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogCallHandler()
            {
                Order = Order,
                Msg = Msg
            };
        }
    }
    public class LogCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //计时开始
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            //执行方法
            IMethodReturn result = getNext()(input, getNext);

            //计时结束
            stopWatch.Stop();

            //记录日志

            var argumentsSb = new StringBuilder(input.MethodBase.Name);
            for (var i = 0; i < input.Arguments.Count; i++)
            {
                argumentsSb.AppendFormat("-{0}:{1}", input.Arguments.ParameterName(i), input.Arguments[i]);
            }
            Logger.LogInfo(string.Format("{2} 方法 {0},执行时间 {1} ms", argumentsSb, stopWatch.ElapsedMilliseconds, Msg));
            return result;
        }

        public int Order { get; set; }
        public string Msg { get; set; }
    }
}
