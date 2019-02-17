using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> list, int size)
    {
        while (list.Any())
        {
            yield return list.Take(size);
            list = list.Skip(size);
        }
    }
}