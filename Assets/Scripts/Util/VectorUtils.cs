using UnityEngine;
using static UnityEngine.Mathf;

public static class VectorUtils
{
    public static float Vec2ToRad(Vector2 vector)
    {
        return Atan2(vector.x, vector.y);
    }

    public static Vector2 RadToVec2(float radians)
    {
        return new Vector2(Cos(radians), Sin(radians));
    }

    public static Vector2 RotateByDegrees(Vector2 vector, float degrees)
    {
        return RotateByRadians(vector, degrees * Deg2Rad);
    }

    public static Vector2 RotateByRadians(Vector2 vector, float radians)
    {
        float cos = Cos(radians);
        float sin = Sin(radians);
        return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
    }
}
