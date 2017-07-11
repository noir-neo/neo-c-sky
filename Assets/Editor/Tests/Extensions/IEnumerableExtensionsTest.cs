using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class IEnumerableExtensionsTest
{
    public class Test
    {
        public int a;
        public int b;

        public Test(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }

    [Test]
    public static void BindOrderBy()
    {
        var test11 = new Test(1, 1);
        var test12 = new Test(1, 2);
        var test21 = new Test(2, 1);
        var test22 = new Test(2, 2);

        var list = new List<Test>
        {
            test22, test21, test12, test11
        };

        var expected = new List<List<Test>>
        {
            new List<Test>
            {
                test11, test12
            },
            new List<Test>
            {
                test21, test22
            }
        };

        CollectionAssert.AreEqual(expected, list.BindOrderBy(t => t.a, t => t.b));
    }
}