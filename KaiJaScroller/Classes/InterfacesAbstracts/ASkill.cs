using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class ASkill
{
    protected Entity entity;

    public String name;

    protected IActionListener action;

    protected double curCooldown;
    protected double maxCooldown;

    public double Cooldown
    {
        get { return maxCooldown; }
        set { curCooldown = value; maxCooldown = value; }
    }

    public void update(GameTime gameTime)
    {


        if (curCooldown > 0)
            curCooldown -= gameTime.ElapsedTime.TotalSeconds;

        else if (curCooldown <= 0 && action.performed(gameTime, entity, name))
        {
            curCooldown = maxCooldown;
            onUse(gameTime);
        }
    }

    public void setEntity(Entity e)
    {
        this.entity = e;
    }

    public void setAction(IActionListener action)
    {
        this.action = action;
    }



    public abstract void onUse(GameTime gameTime);
}