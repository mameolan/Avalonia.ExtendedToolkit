using System;
using System.Collections.Generic;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// extensions for IEnumerable
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// executes an action per item
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void ForEach(this IEnumerable<object> list, Action<object> action)
        {
            foreach (var item in list)
            {
                action(item);
            }

        }

        /// <summary>
        /// executes an action per item
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }

        }
    }
}
