using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Utils
{
    /// <summary>
    /// 类型的比较器
    /// </summary>
    public class TypeNameComparer : IComparer<Type>
    {
        public static readonly TypeNameComparer Instance = new TypeNameComparer();

        private TypeNameComparer() { }

        /// <summary>
        /// TypeNameComparer 先尝试使用Name来比较，如果一样，再使用NameSpace进行比较。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int IComparer<Type>.Compare(Type x, Type y)
        {
            var result = x.Name.CompareTo(y.Name);
            if (result == 0)
            {
                result = x.Namespace.CompareTo(y.Namespace);
            }
            return result;
        }
    }
}
