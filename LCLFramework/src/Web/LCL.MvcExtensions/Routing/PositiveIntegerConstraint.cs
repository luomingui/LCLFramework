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

namespace LCL.MvcExtensions
{
    public class PositiveIntegerConstraint : RangeConstraint<int>
    {
        public PositiveIntegerConstraint() : this(false)
        {
        }

        public PositiveIntegerConstraint(bool optional) : base(1, int.MaxValue, optional)
        {
        }
    }
}