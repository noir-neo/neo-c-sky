using NeoC.Game.Model;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class SquareModelExtensionsTest
{
    [Test]
    public static void RotateSquareModelRotation()
    {
        RotateTest(SquareModel.up, SquareModel.bottom, SquareModel.right);
        RotateTest(SquareModel.up, SquareModel.up, SquareModel.left);
        RotateTest(SquareModel.right, SquareModel.left, SquareModel.left);
        RotateTest(SquareModel.up, SquareModel.upRight, SquareModel.upLeft);
        RotateTest(SquareModel.bottomLeft, SquareModel.upRight, SquareModel.bottom);
        RotateTest(SquareModel.bottomLeft, SquareModel.up, SquareModel.bottomRight);
        RotateTest(SquareModel.zero, SquareModel.up, SquareModel.zero);
    }

    public static void RotateTest(SquareModel model, SquareModel rotation, SquareModel expected)
    {
        model = model.Rotate(rotation);
        Assert.AreEqual(expected, model, $"Expected: ({expected.X}, {expected.Y})\nBut was:  SquareModel({model.X}, {model.Y})");
    }
}