using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class Smoke : AParticleBehavior
{
    Vector2f dir;
    float speed;
    float rotateSpeed;

    public Smoke()
    {

    }


    public override void init()
    {
        dir = Vec2f.lerp(new Vector2f(0.5f, -1), new Vector2f(-0.5f, -1), (float)Help.random.NextDouble());

        speed = (float)Help.random.NextDouble();
        rotateSpeed = speed;

        if (Help.random.NextDouble() < 0.5)
            rotateSpeed *= -1;



        float help = Math.Max(1.5f * (float)Help.random.NextDouble(), 0.75f);

        this.parent.sprite.Scale = new Vector2f(help, help);

        this.lifeTime = 2;
    }

    public override void update(GameTime gametime)
    {
        this.parent.sprite.Position += dir * speed;
        this.parent.sprite.Rotation += 5 * rotateSpeed;

        this.parent.sprite.Color = Help.fade(this.parent.sprite.Color, Math.Min(1, this.parent.lifeTime));
    }
}

