using NeoC.Game.Model;
using UnityEngine;

public static class SquareModelExtensions
{
    public static SquareModel Rotate(this SquareModel squareModel, SquareModel rotation)
    {
        var vector = (Vector2) rotation;
        float sin = vector.normalized.y;
        float cos = vector.normalized.x;
        return new SquareModel(Mathf.RoundToInt(cos * squareModel.X - sin * squareModel.Y),
            Mathf.RoundToInt(sin * squareModel.X + cos * squareModel.Y));
    }

    public static float EulerAngles(this SquareModel squareModel)
    {
        return Vector2.Angle(Vector2.right, squareModel);
    }
}