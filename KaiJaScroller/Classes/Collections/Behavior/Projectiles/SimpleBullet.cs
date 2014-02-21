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

    public SimpleBullet()
    {
    }

    public override void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;


        if (speed > 0)
        {
            if (this.entity.canMoveRight(speed))
                this.entity.position += new Vector2f(speed, 0);
            else
                this.entity.exists = false;
        }

        else if (speed < 0)
        {
            if (this.entity.canMoveLeft(speed))
                this.entity.position += new Vector2f(speed, 0);
            else
                this.entity.exists = false;
        }
   

        if (lifeTime <= 0)
            this.entity.exists = false;
    }

}

