using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    public static IOrderedEnumerable<IOrderedEnumerable<T>> BindOrderBy<T, Tkey>(this List<T> source, Func<T, Tkey> primaryOrder, Func<T, Tkey> secondaryOrder)
    {
        return source.GroupBy(primaryOrder)
            .GroupBy(g => g.Key,
                g => g.OrderBy(secondaryOrder))
            .SelectMany(i => i)
            .OrderBy(l => primaryOrder(l.First()));
    }
}