/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.MvcExtensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RouteNameAttribute : Attribute
    {
        public RouteNameAttribute(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
