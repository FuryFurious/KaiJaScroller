using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class ABrain
{
    protected Entity entity;

    public void setEntity(Entity e)
    {
        this.entity = e;
    }

    public abstract void think(GameTime gameTime);

}
