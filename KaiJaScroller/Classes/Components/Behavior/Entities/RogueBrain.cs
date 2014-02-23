using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RogueBrain : ABehavior , IActionListener
{
    //Brains:
    ChaseBrain chase;


    //Skills:
    SmallSword sword;
    Bomb bomb;



    public RogueBrain()
    {

    }

    public override void init()
    {
        chase = new ChaseBrain();
        chase.setEntity(this.entity);

        chase.chaseRangeX = 100;

        sword = new SmallSword();
        sword.setAction(this);
        sword.setEntity(this.entity);

        bomb = new Bomb();
        bomb.setEntity(this.entity);
        bomb.setAction(this);

        bomb.Cooldown = 0.75;
        
    }

    public override void update(GameTime gameTime)
    {
        
        chase.update(gameTime);

        sword.update(gameTime);
        bomb.update(gameTime);

    }

    public override void onKill()
    {
        
    }

    public bool performed(GameTime gameTime, Entity source, string name)
    {
        if (name == sword.name && Help.distance(source.boundingBox.CenterX, source.ingame.player.boundingBox.CenterX) < 48)
            return true;
        
        else if (name == bomb.name)
            return true;
        

        return false;
    }
}

