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
    private Vector2f startPos;

    public SimpleBullet(Vector2f start)
    {
       this.startPos = start;
    }

    public override void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        this.entity.position += new Vector2f(speed, 0);

        if (lifeTime <= 0)
            this.entity.exists = false;
    }

    public override void init()
    {
        this.entity.position = startPos;
    }
}

