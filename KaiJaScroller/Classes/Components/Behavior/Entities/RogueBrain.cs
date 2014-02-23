using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RogueBrain : ABehavior , IActionListener
{
    ChaseBrain chase;
    SmallSword sword;

    public RogueBrain()
    {

    }

    public override void init()
    {
        chase = new ChaseBrain();

        chase.setEntity(this.entity);

        sword = new SmallSword();
        sword.setAction(this);
        sword.setEntity(this.entity);
    }

    public override void update(GameTime gameTime)
    {
    //    chase.update(gameTime);
        sword.update(gameTime);

    }

    public override void onKill()
    {
        
    }

    public bool performed(GameTime gameTime, Entity source, string name)
    {
        if (name == sword.name)
        {

            return true;
        }
        return false;
    }
}

