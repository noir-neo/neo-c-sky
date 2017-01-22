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

    public static void LookToward(this Transform transform, Vector3 move)
    {
        transform.LookToward(move, 360.0f * Time.deltaTime);
    }

    public static void LookToward(this Transform transform, Vector3 move, float maxDegreesDelta)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(move), maxDegreesDelta);
    }

    public static Vector3 X0Y(this Vector3 vector3)
    {
        vector3.z = vector3.y;
        vector3.y = 0;
        return vector3;
    }

    public static Vector3 X0Y(this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }

    public static void PositionLerp(this Transform transform, Vector3 target, float t)
    {
        transform.position = Vector3.Lerp(transform.position, target, t);
    }

    public static float LimitDirection(this float source, int limit)
    {
        float unit =  360.0f / limit;
        float remainder = source % unit;

        if (remainder > unit / 2)
        {
            return source + unit - remainder;
        }
        else
        {
            return source - remainder;
        }
    }

    public static Vector2 LimitDirection(this Vector2 source, int limit)
    {
        var angle = Mathf.Atan2(source.y, source.x) * 180 / Mathf.PI;
        return angle.LimitDirection(limit).RadianToVector2();
    }

    public static Vector2 RadianToVector2(this float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(this float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
