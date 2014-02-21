using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Help
{
    public static void drawRectangle(FloatRect fRect, RenderTarget target)
    {
        RectangleShape shape = new RectangleShape(new Vector2f(fRect.Width, fRect.Height));
        shape.Position = new Vector2f(fRect.Left, fRect.Top);

        //transparent red:
        shape.FillColor = new Color(255, 0, 0, 127);

        target.Draw(shape);
    }
}

