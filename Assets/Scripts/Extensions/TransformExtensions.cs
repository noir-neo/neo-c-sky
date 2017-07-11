using UnityEngine;

public static class TransformExtensions
{
    public static void Rotate(this Transform transform, Vector3 axis, float angle, float minAngle, float maxAngle, Space space = Space.Self)
    {
        var currentAngle = transform.EulerAngle(axis);

        var newAngle = ClampAngle(currentAngle + angle, minAngle, maxAngle);
        angle = newAngle - currentAngle;
        transform.Rotate(axis, angle, space);
    }

    public static float EulerAngle(this Transform transform, Vector3 axis)
    {
        return Vector3.Scale(transform.eulerAngles, axis.normalized).magnitude;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle += 180;
        angle = Wrap(angle, 360);
        angle -= 180;
        angle = Mathf.Clamp(angle, min, max);

        return angle;
    }

    public static float Wrap(float t, float length)
    {
        while (t < 0)
            t += length;
        return Mathf.Repeat(t, length);
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

    public static void LookToward(this Rigidbody rigidbody, Vector3 move)
    {
        rigidbody.LookToward(move, 360.0f * Time.deltaTime);
    }

    public static void LookToward(this Rigidbody rigidbody, Vector3 move, float maxDegreesDelta)
    {
        rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation,
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

    public static Vector2 XZ(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }

    public static void Move(this Transform transform, Vector3 delta)
    {
        var pos = transform.position;
        pos += delta;
        transform.position = pos;
    }

    public static void Scale(this Transform transform, float value)
    {
        transform.localScale += Vector3.one * value;
    }

    public static void Scale(this Transform transform, float value, float min, float max)
    {
        var scale = transform.localScale;
        for (int i = 0; i < 3; i++)
        {
            scale[i] = Mathf.Clamp(scale[i] + value, min, max);
        }
        transform.localScale = scale;
    }

    public static void PositionLerp(this Transform transform, Vector3 target, float t)
    {
        transform.position = Vector3.Lerp(transform.position, target, t);
    }

    public static void UpdatePositionLerp(this Transform transform, float startTime, float speed, float journeyLength, Vector3 startPos, Vector3 endPos)
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
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

    public static Vector2 Clamp(this Vector2 value, Vector4 leftTopRightBottom)
    {
        return Clamp(value, leftTopRightBottom.x, leftTopRightBottom.y, leftTopRightBottom.z, leftTopRightBottom.w);
    }

    public static Vector2 Clamp(this Vector3 value, Vector4 leftTopRightBottom)
    {
        return Clamp(value, leftTopRightBottom.x, leftTopRightBottom.y, leftTopRightBottom.z, leftTopRightBottom.w);
    }

    public static Vector2 Clamp(Vector2 value, float left, float top, float right, float bottom)
    {
        if (value.x < left) value.x = left;
        if (top < value.y) value.y = top;
        if (right < value.x) value.x = right;
        if (value.y < bottom) value.y = bottom;
        return value;
    }

    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
    {
        for (int i = 0; i < 3; i++)
        {
            value[i] = Mathf.Clamp(value[i], min[i], max[i]);
        }
        return value;
    }
}
