using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SchadLucas.EatSmart.Extensions.System.Collections
{
    public static class IEnumerableExtensions
    {
        public static List<T> ToList<T>(this IEnumerable enumerable)
        {
            return enumerable.Cast<T>().ToList();
        }
    }
}