using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimpleBullet : ABehavior
{
    public double lifeTime = 5;
    public float speed = 3;
    EDirection dir;


    public SimpleBullet(EDirection direction)
    {
        this.dir = direction;
    }

    public override void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        float nextSpeed = speed + 20 * speed * (1 - ((float)lifeTime / 5.0f));

        if (dir == EDirection.Right)
        {
            if (this.entity.canMoveRight(nextSpeed, 0))
                this.entity.moveRight(nextSpeed);
            else
                this.entity.exists = false;
        }

        else if (dir == EDirection.Left)
        {
            if (this.entity.canMoveLeft(nextSpeed, 0))
                this.entity.moveLeft(nextSpeed);
            else
                this.entity.exists = false;
        }
   

        if (lifeTime <= 0)
            this.entity.exists = false;
    }


    public override void onKill()
    {
        Vector2f startPos = this.entity.boundingBox.Center;
        this.entity.ingame.particles.Add(EntityLibrary.getParticle(EParticleType.Smoke, startPos));
        this.entity.ingame.particles.Add(EntityLibrary.getParticle(EParticleType.Smoke, startPos));
        this.entity.ingame.particles.Add(EntityLibrary.getParticle(EParticleType.Smoke, startPos));
    }

    public override void init()
    {
      
    }
}

