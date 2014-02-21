using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RandomBrain : ABrain
{
    double thinkCooldown = 3;
    float speed = 3;

    bool left = true;


    public override void think(GameTime gameTime)
    {
        thinkCooldown -= gameTime.ElapsedTime.TotalSeconds;

        if (thinkCooldown <= 0)
        {
            left = Help.random.NextDouble() < 0.5;
            thinkCooldown = 5;
        }

        if (left && this.entity.canMoveLeft(-3))
            this.entity.moveHorz(-3);

        else if(this.entity.canMoveRight(3))
            this.entity.moveHorz(3);
    }
}
