using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class AParticleBehavior
{
    public Particle parent;
    public double lifeTime;

    public abstract void init();
    public abstract void update(GameTime gametime);
}

