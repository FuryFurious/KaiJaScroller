using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BoundingBox
{
    FloatRect rectangle;

    public BoundingBox(float x, float y, float width, float height)
    {
        this.rectangle = new FloatRect(x, y, width, height);
    }

    // ______________________ GETTER AND SETTER____________________________

    public float X
    {
        get { return rectangle.Left; }
        set { rectangle.Left = value; }
    }

    public float Y
    {
        get { return rectangle.Top; }
        set { rectangle.Top = value; }
    }

    public float Width
    {
        get { return rectangle.Width; }
        set { rectangle.Width = value; }
    }

    public float Height
    {
        get { return rectangle.Height; }
        set { rectangle.Height = value; }
    }

    public float Left
    {
        get { return rectangle.Left; }
    }

    public float Top
    {
        get { return rectangle.Top; }
    }

    public float Bottom
    {
        get { return rectangle.Top + rectangle.Height; }
    }

    public float Right
    {
        get { return rectangle.Left + rectangle.Width; }
    }


    //__________________________________ METHODS ___________________________

    public bool intersects(BoundingBox b)
    {
        return this.rectangle.Intersects(b.rectangle);
    }

    public bool intersects(FloatRect rect)
    {
        return this.rectangle.Intersects(rect);
    }


    public bool contains(float x, float y)
    {
        return this.rectangle.Contains(x, y);
    }

    public bool contains(Vector2f f)
    {
        return this.rectangle.Contains(f.X, f.Y);
    }

    public bool intersectsHorzLine(float y, float x0, float x1)
    {
        if (x0 > x1)
            Help.swap(ref x0, ref x1);

        float right = this.Right;
        //x0 is inside, x1 isInside, both are outside
        return  (y <= this.Bottom && y >= this.Top) 
            && (
                        (x0 >= this.Left && x0 <= right)
                    ||  (x1 >= this.Left && x1 <= right)
                    ||  (x0 <= this.Left && x1 >= right)  );
    }

    public bool intersectsVertLine(float x, float y0, float y1)
    {
        if (y0 > y1)
            Help.swap(ref y0, ref y1);

        float bot = this.Bottom;

        return (x <= this.Right && x >= this.Left) && 
                ( 
                    (y0 >= this.Top && y0 <= bot) || 
                    (y1 >= this.Top && y1 <= bot) || 
                    (y0 >= this.Top && y1 <= bot));
    }





    public void draw(RenderTarget target)
    {
        RectangleShape shape = new RectangleShape(new Vector2f(rectangle.Width, rectangle.Height));
        shape.Position = new Vector2f(rectangle.Left, rectangle.Top);

        //transparent red:
        shape.FillColor = new Color(255, 0, 0, 127);

        target.Draw(shape);
    }


}

