using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RandomBrain : ABehavior
{
    double thinkCooldown = 3;

    bool moveLeft = true;


    public override void update(GameTime gameTime)
    {
        thinkCooldown -= gameTime.ElapsedTime.TotalSeconds;

        if (thinkCooldown <= 0)
        {
            moveLeft = Help.random.NextDouble() < 0.5;
            thinkCooldown = 5;
        }

        if (moveLeft && this.entity.canMoveLeft(-3))
            this.entity.moveHorz(-3);

        else if(!moveLeft && this.entity.canMoveRight(3))
            this.entity.moveHorz(3);
    }

    public override void onKill()
    {

    }


    public override void init()
    {
        
    }
}
