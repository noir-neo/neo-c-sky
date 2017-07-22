using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class ListExtensionsTest
{
    [Test]
    public static void StretchLength()
    {
        var list = new List<int>
        {
            0, 1, 2
        };

        list.StretchLength(4);
        CollectionAssert.AreEqual(new List<int>{ 0, 1, 2, 0 }, list);

        list.StretchLength(2);
        CollectionAssert.AreEqual(new List<int>{ 0, 1 }, list);
    }
}