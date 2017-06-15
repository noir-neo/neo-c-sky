using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class ComponentExtensionsTest
{
    [Test]
    public static void TryGetComponentWhenExistsReturnTrue()
    {
        var gameObject = new GameObject();
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();

        BoxCollider outBoxCollider;
        Assert.IsTrue(rigidbody.TryGetComponent(out outBoxCollider));
    }

    [Test]
    public static void TryGetComponentWhenExistsOutComponent()
    {
        var gameObject = new GameObject();
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        var boxCollider = gameObject.AddComponent<BoxCollider>();

        BoxCollider outBoxCollider;
        rigidbody.TryGetComponent(out outBoxCollider);
        Assert.AreEqual(outBoxCollider, boxCollider);
    }

    [Test]
    public static void TryGetComponentWhenNotExistsReturnFalse()
    {
        var gameObject = new GameObject();
        var rigidbody = gameObject.AddComponent<Rigidbody>();

        BoxCollider outBoxCollider;
        Assert.IsFalse(rigidbody.TryGetComponent(out outBoxCollider));
    }

    [Test]
    public static void TryGetComponentWhenNotExistsOutNull()
    {
        var gameObject = new GameObject();
        var rigidbody = gameObject.AddComponent<Rigidbody>();

        BoxCollider outBoxCollider;
        rigidbody.TryGetComponent(out outBoxCollider);
        Assert.AreEqual(outBoxCollider, null);
    }
}