using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap
{
    public class TiledRectangle
    {
        public float x; 
        public float y; 
        public float width;
        public float height;

        public String name;
        public String type;
 
        public TiledRectangle()
        {
        }

        public TiledRectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public override String ToString()
        {
            return "Name: "+ name + "Type: " + type + ", X: " + x + ", Y: " + y + ", Width: " + width + ", Height: " + height;
        }
    }
}
