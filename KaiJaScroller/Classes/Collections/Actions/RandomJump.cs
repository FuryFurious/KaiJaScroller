using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RandomJump : IAction
{

    double cooldown = 4;

    public bool performed(GameTime gameTime, InGame ingame)
    {

        cooldown -= gameTime.ElapsedTime.TotalSeconds;

        if (cooldown <= 0)
        {
            if (Help.random.NextDouble() < 0.5)
            {
                cooldown = 5;
                return true;
            }
           
        }

        return false;
    }
}

