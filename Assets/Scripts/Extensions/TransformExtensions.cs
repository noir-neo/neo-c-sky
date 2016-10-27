using UnityEngine;

public static class TransformExtensions
{
    public static void Rotate(this Transform transform, Vector3 axis, float angle, float minAngle, float maxAngle, Space space = Space.Self)
    {
        var currentAngle = Vector3.Scale(transform.eulerAngles, axis.normalized).magnitude;

        minAngle += 180;
        maxAngle += 180;
        currentAngle += 180;
        angle += currentAngle;

        while (angle < 0) {
            angle += 360;
        }
        angle = Mathf.Repeat(angle, 360);

        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        angle -= currentAngle;

        transform.Rotate(axis, angle, space);
    }
}
