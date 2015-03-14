/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System.Diagnostics;
namespace LCL.MvcExtensions
{
    internal static class KnownActionNames
    {
        public const string Index = "index";
        public const string Show = "show";
        public const string New = "new";
        public const string Create = "create";
        public const string Edit = "edit";
        public const string Update = "update";
        public const string Destroy = "delete";

        private static readonly string[] all = new[] { Index, Show, New, Create, Edit, Update, Destroy };
        private static readonly string[] createAndUpdate = new[] { Create, Update };

        [DebuggerStepThrough]
        public static string[] All()
        {
            return all;
        }

        [DebuggerStepThrough]
        public static string[] CreateAndUpdate()
        {
            return createAndUpdate;
        }
    }
}