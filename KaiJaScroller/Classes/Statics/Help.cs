using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Help
{
    

    public const uint A = 0;
    public const uint B = 1;
    public const uint X = 2;
    public const uint Y = 3;
    public const uint LB = 4;
    public const uint RB = 5;
    public const uint Select = 6;
    public const uint Start = 7;
    public const uint LS = 8;
    public const uint RS = 9;

    public static void drawRectangle(FloatRect fRect, RenderTarget target)
    {
        RectangleShape shape = new RectangleShape(new Vector2f(fRect.Width, fRect.Height));
        shape.Position = new Vector2f(fRect.Left, fRect.Top);

        //transparent red:
        shape.FillColor = new Color(255, 0, 0, 127);

        target.Draw(shape);
    }

    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}

