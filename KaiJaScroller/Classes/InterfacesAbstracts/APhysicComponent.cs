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

    public void jump(float power)
    {
        (this as SimplePhysic).jump(power);
    }

    public abstract void init();

    public abstract void update(GameTime gameTime);
}

