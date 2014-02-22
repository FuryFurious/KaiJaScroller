using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Teleporter
{
    public BoundingBox bb;
    public String targetMap;

    public Teleporter(float x, float y, float width, float height, String map)
    {
        this.bb = new BoundingBox(x, y, width, height);
        this.targetMap = map;
    }

    public void draw(RenderTarget target)
    {
        bb.draw(target);
    }
}
