using System.Collections.Generic;

public static class ListExtensions
{
    public static void StretchLength<T>(this List<T> list, int length)
    {
        while (list.Count < length)
        {
            list.Add(default(T));
        }
        while (list.Count > length)
        {
            list.RemoveAt(length);
        }
    }
}