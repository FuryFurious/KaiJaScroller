using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class CombatComponent
{
    Entity entity;


    public void setEntity(Entity e)
    {
        this.entity = e;
    }
}