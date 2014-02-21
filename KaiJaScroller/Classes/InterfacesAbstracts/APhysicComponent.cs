using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class APhysicComponent
{
    protected Entity entity;

    public void setEntity(Entity e)
    {
        this.entity = e;
    }

    public abstract void update(GameTime gameTime);
}

