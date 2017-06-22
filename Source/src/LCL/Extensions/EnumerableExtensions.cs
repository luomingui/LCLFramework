using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LCL
{

    /// <summary>
    /// Defines an static class which contains extension methods of <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
        public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any();
        }
        /// <summary>
        /// Executes the provided action for each item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="action">The action.</param>
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            if (instance == null)
            {
                return;
            }
            foreach (T item in instance)
            {
                action(item);
            }
        }
        //改进的Aggerate扩展（示例代码，实际使用请添加空值检查）
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func)
        {
            int index = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                index++;
                TSource current = enumerator.Current;
                while (enumerator.MoveNext())
                    current = func(current, enumerator.Current, index++);
                return current;
            }
        }
    }
}