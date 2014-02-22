using SFML.Window;
using System;

public static class Vec2f
{

    public static float dot(Vector2f a, Vector2f b)
    {
        return a.X * b.X + a.Y * b.Y;
    }

    public static float length(Vector2f a)
    {
        return (float)Math.Sqrt(lengthSqr(a));
    }

    public static float lengthSqr(Vector2f a)
    {
        return a.X * a.X + a.Y * a.Y;
    }

    public static float dist(Vector2f a, Vector2f b)
    {
        return length(b - a);
    }

    public static float distSqr(Vector2f a, Vector2f b)
    {
        return lengthSqr(b - a);
    }

    public static Vector2f lerp(Vector2f a, Vector2f b, float t)
    {
        return (1.0f - t) * a + t * b;
    }


}
