using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Caching;

namespace LCL.Caching
{
    /// <summary>
    /// 缓存策略
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// 一个空策略。
        /// </summary>
        public static readonly Policy Empty = new Policy();

        /// <summary>
        /// 缓存使用的实时检测器
        /// </summary>
        public ChangeChecker Checker { get; set; }
    }
}
