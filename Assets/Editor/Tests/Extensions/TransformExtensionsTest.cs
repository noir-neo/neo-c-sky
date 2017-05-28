using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class TransformExtensionsTest
{
    [Test]
    public void Rotate()
    {
        var min = -60f;
        var max = 80f;
        var delta = 0.01f;

        var gameObject1 = new GameObject();
        gameObject1.transform.Rotate(Vector3.right, 30f, min, max);
        Assert.AreEqual(gameObject1.transform.rotation.eulerAngles.x, 30f, delta);

        gameObject1.transform.Rotate(Vector3.right, 60f, min, max);
        Assert.AreEqual(gameObject1.transform.rotation.eulerAngles.x, 80f, delta);

        gameObject1.transform.Rotate(Vector3.right, -130f, min, max);
        Assert.AreEqual(gameObject1.transform.rotation.eulerAngles.x, 310f, delta);

        gameObject1.transform.Rotate(Vector3.right, -100f, min, max);
        Assert.AreEqual(gameObject1.transform.rotation.eulerAngles.x, 300f, delta);

        // gameObject1.transform.Rotate(Vector3.right, -121f, min, max);
        gameObject1.transform.Rotate(Vector3.right, -120f, min, max);
        Assert.AreEqual(gameObject1.transform.rotation.eulerAngles.x, 300f, delta);
    }

    [Test]
    public void EulerAngle()
    {
        var gameObject = new GameObject();
        var transform = gameObject.transform;
        Assert.AreEqual(transform.EulerAngle(Vector3.right), transform.eulerAngles.x);

        var eulerAngles = transform.eulerAngles;
        eulerAngles.x += 30;
        transform.eulerAngles = eulerAngles;
        Assert.AreEqual(transform.EulerAngle(Vector3.right), transform.eulerAngles.x);
    }

    [Test]
    public void ClampAngle()
    {
        var min = -60f;
        var max = 80f;
        Assert.AreEqual(TransformExtensions.ClampAngle(10f, min, max), 10f);
        Assert.AreEqual(TransformExtensions.ClampAngle(90f, min, max), 80f);
        Assert.AreEqual(TransformExtensions.ClampAngle(179f, min, max), 80f);
        // Assert.AreEqual(TransformExtensions.ClampAngle(180f, min, max), 80f);
        Assert.AreEqual(TransformExtensions.ClampAngle(-50f, min, max), -50f);
        Assert.AreEqual(TransformExtensions.ClampAngle(-60f, min, max), -60f);
        Assert.AreEqual(TransformExtensions.ClampAngle(-80f, min, max), -60f);

        min = 0;
        Assert.AreEqual(TransformExtensions.ClampAngle(-80f, min, max), 0);
        Assert.AreEqual(TransformExtensions.ClampAngle(-180f, min, max), 0);
        // Assert.AreEqual(TransformExtensions.ClampAngle(-181f, min, max), 0);
    }

    [Test]
    public void Wrap()
    {
        Assert.AreEqual(TransformExtensions.Wrap(180f, 360f), 180f);
        Assert.AreEqual(TransformExtensions.Wrap(-90f, 360f), 270f);
        Assert.AreEqual(TransformExtensions.Wrap(400f, 360f), 40f);
    }

    [Test]
    public void ScaleValue()
    {
        TestScale(new GameObject().transform, 1, 2);
        TestScale(new GameObject().transform, -1, 0);
        
        var transform = new GameObject().transform;
        TestScale(transform, 0.1f, 1.1f);
        TestScale(transform, 0.1f, 1.2f);
        TestScale(transform, 0.2f, 1.4f);
    }
    
    public void TestScale(Transform transform, float value, float actual)
    {
        transform.Scale(value);
        AreEqual(transform.localScale, new Vector3(actual, actual, actual));
    }

    [Test]
    public void ScaleValueMinMax()
    {
        TestScale(new GameObject().transform, 0.1f, 0, 2, 1.1f);
        TestScale(new GameObject().transform, 3, 0, 2, 2f);
        TestScale(new GameObject().transform, -1, 0.5f, 2, 0.5f);

        var transform = new GameObject().transform;
        TestScale(transform, 0.1f, 0.5f, 2, 1.1f);
        TestScale(transform, 0.1f, 0.5f, 2, 1.2f);
        TestScale(transform, 0.2f, 0.5f, 2, 1.4f);
    }
    
    public void TestScale(Transform transform, float value, float min, float max, float actual)
    {
        transform.Scale(value, min, max);
        AreEqual(transform.localScale, new Vector3(actual, actual, actual));
    }

    public static void AreEqual(Vector3 expected, Vector3 actual, float delta = 0.0001f)
    {
        var message = string.Format("Expected: Vector3({0}, {1}, {2})\nBut was:  Vector3({3}, {4}, {5})",
            expected.x, expected.y, expected.z,
            actual.x, actual.y, actual.z);
        float sqrMagnitude = Vector3.SqrMagnitude(expected - actual);
        Assert.LessOrEqual(sqrMagnitude, delta, message);
    }
}
