using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class ABehavior
{
    protected Entity entity;


    public void setEntity(Entity e)
    {
        this.entity = e;
    }

    //TODO: add onKill function();
    public abstract void update(GameTime gameTime);
}
