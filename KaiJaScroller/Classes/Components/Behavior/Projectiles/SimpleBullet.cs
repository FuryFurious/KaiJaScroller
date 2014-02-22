﻿using SFML.Graphics;
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

    public SimpleBullet(EDirection direction)
    {
        if (direction == EDirection.Left)
            speed = -3;
        else
            speed = 3;
    }

    public override void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        float nextSpeed = speed + 20 * speed * (1 - ((float)lifeTime / 5.0f));

        if (speed > 0)
        {
            if (this.entity.canMoveRight(nextSpeed))
                this.entity.moveHorz(nextSpeed);
            else
                this.entity.exists = false;
        }

        else if (speed < 0)
        {
            if (this.entity.canMoveLeft(nextSpeed))
                this.entity.moveHorz(nextSpeed);
            else
                this.entity.exists = false;
        }
   

        if (lifeTime <= 0)
            this.entity.exists = false;
    }


    public override void onKill()
    {
        Sprite s1 = new Sprite(Assets.flameParticle);

        s1.Scale = new Vector2f(0.5f, 0.5f);
        s1.Rotation = (float)(Help.random.NextDouble() * 360.0);

        Particle p = new Particle(Help.random.NextDouble() * 2);

        p.setBehavior(new RandomFloat());
        p.setSprite(s1);
        p.setPosition(this.entity.position);

        this.entity.ingame.particles.Add(p);
    }

    public override void init()
    {
        
    }
}

