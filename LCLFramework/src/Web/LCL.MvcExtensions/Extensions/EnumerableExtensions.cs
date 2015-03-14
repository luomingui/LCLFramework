using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LCL.MvcExtensions
{

    /// <summary>
    /// Defines an static class which contains extension methods of <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
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
    }
}