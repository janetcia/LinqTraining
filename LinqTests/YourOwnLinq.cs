using System;
using System.Collections.Generic;
using LinqTests;

namespace LinqSample.YourOwnLinq
{
    internal static class YourOwnLinq
    {
        public static T MyFirstOrDefault<T>(this IEnumerable<T> sources, Func<T, bool> func)
        {
            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current))
                {
                    return enumerator.Current;
                }
            }
            return default(T);
        }

        public static T YourSingle<T>(IEnumerable<T> sources, Func<T, bool> func)
        {
            var enumerator = sources.GetEnumerator();
            var current = enumerator.Current;
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current))
                {
                    current = enumerator.Current;
                    index++;
                    if (index > 1)
                    {
                        break; ;
                    }
                }
            }
            if (index == 0 || index > 1)
            {
                throw new InvalidOperationException();
            }
            return current;
        }

        public static IEnumerable<T> YourDistinct<T>(IEnumerable<T> sources)
        {
            var enumerator = sources.GetEnumerator();

            var hashSet = new HashSet<T>();
            while (enumerator.MoveNext())
            {
                if (hashSet.Add(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }

        }
    }
}