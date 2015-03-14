using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Happy.Infrastructure.ExtentionMethods;
using Happy.Command.Internal;

namespace Happy.Command
{
    /// <summary>
    /// 获取或访问<see cref="ICommandService"/>实例的唯一入口。
    /// </summary>
    public static class CommandService
    {
        private static readonly DefaultCommandService _DefaultCommandService
            = new DefaultCommandService();

        private static CommandServiceProvider currentProvider = DefaultCommandService;

        /// <summary>
        /// 获取当前应用程序正在使用的命令服务。
        /// </summary>
        public static ICommandService Current
        {
            get { return currentProvider(); }
        }

        /// <summary>
        /// 设置当前应用程序正在使用的命令服务提供者。
        /// </summary>
        public static void SetProvider(CommandServiceProvider provider)
        {
            provider.MustNotNull("provider");

            currentProvider = provider;
        }

        /// <summary>
        /// 创建一个默认的命令服务实例，每次调用都会返回不同的实例。
        /// </summary>
        public static ICommandService CreateDefaultCommandService()
        {
            return new DefaultCommandService();
        }

        /// <summary>
        /// 返回内置的默认的命令服务实例，每次调用都会返回同一个实例。
        /// </summary>
        public static ICommandService DefaultCommandService()
        {
            return _DefaultCommandService;
        }
    }
}
