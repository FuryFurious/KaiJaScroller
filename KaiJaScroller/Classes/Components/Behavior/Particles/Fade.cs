using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Fade : AParticleBehavior
{


    public Fade(double lifeTime)
    {
        this.lifeTime = lifeTime;
       
    }

    public override void init()
    {
        this.parent.lifeTime = lifeTime;
    }

    public override void update(GameTime gametime)
    {
        this.parent.sprite.Color = Help.fade(this.parent.sprite.Color, Math.Min(1, this.parent.lifeTime));
    }
}
