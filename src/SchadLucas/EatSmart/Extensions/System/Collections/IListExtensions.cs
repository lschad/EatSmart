using System;
using System.Collections;

namespace SchadLucas.EatSmart.Extensions.System.Collections
{
    // ReSharper disable once InconsistentNaming
    public static class IListExtensions
    {
        public static void ForEach<T>(this IList enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}