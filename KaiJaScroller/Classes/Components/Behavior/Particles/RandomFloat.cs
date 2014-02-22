using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RandomFloat : AParticleBehavior
{

    Vector2f dir = new Vector2f(0,0);

    public override void update(GameTime gametime)
    {
        this.parent.sprite.Position += dir;
    }

    public override void init()
    {
        double rand1 = Help.random.NextDouble();
        double rand2 = Help.random.NextDouble();

        if (rand1 < 0.33)
            dir.X = 1;

        else if(rand1 < 0.66)
            dir.X = -1;

        if (rand2 < 0.33)
            dir.Y = 1;

        else if(rand2 < 0.66)
            dir.Y = -1;

    }
}
