﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Help
{
    public static Random random = new Random();
    public static MyReader reader = new MyReader();

    public const uint A         = 0;
    public const uint B         = 1;
    public const uint X         = 2;
    public const uint Y         = 3;
    public const uint LB        = 4;
    public const uint RB        = 5;
    public const uint Select    = 6;
    public const uint Start     = 7;
    public const uint LS        = 8;
    public const uint RS        = 9;




    public static void swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static Color fade(Color c1, double t)
    {
        return new Color(c1.R, c1.G, c1.B, (byte)(c1.A * Math.Min(t, 1)));
    }

    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    public static Color lerp(Color c1, Color c2, float t)
    {
        float s = (1.0f - t);

        byte red    = (byte)(s * (float)c1.R + t * (float)c2.R);
        byte green  = (byte)(s * (float)c1.G + t * (float)c2.G);
        byte blue   = (byte)(s * (float)c1.B + t * (float)c2.B);
        byte alpha  = (byte)(s * (float)c1.A + t * (float)c2.A);

        return new Color(red, green, blue, alpha);
    }

    public static float distance(float x, float y)
    {
        return (float)Math.Abs(x-y);
    }
}

