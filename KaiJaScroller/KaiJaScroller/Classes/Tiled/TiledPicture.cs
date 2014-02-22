using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap
{
    public class TiledPicture
    {
        public float x;
        public float y;
        public int id;

        public String type;

        public TiledPicture()
        {
        }

        public TiledPicture(float x, float y, int id)
        {
            this.x = x;
            this.y = y;
            this.id = id;   
        }
        public override String ToString()
        {
            return "Type: " + type + ", X: " + x + ", Y: " + y + ", ID: " + id ;
        }
    }


}
