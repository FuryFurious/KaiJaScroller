using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimpleSword : ABehavior
{
    public float xSpeed;

    public double lifeTime;

    public float count;
    public float reach;

    public Entity e;
    
    


    public SimpleSword(EDirection direction)
    {

    }

    public override void init()
    {

    }

    public override void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        if (count < reach)
        {
            count += 2f;
        }

        if (e.direction != this.entity.direction)
        {
            this.entity.direction = e.direction;
            this.entity.gfxComp.mirror();
        }

        if (this.entity.direction == EDirection.Left)
                 this.entity.setPosition(e.boundingBox.CenterX - 20 - count, e.boundingBox.CenterY - 13);

        else if (this.entity.direction == EDirection.Right)
                this.entity.setPosition(e.boundingBox.CenterX - 12 + count, e.boundingBox.CenterY - 13);
        
        if (lifeTime <= 0)
            this.entity.exists = false;
    }

    public override void onKill()
    {
        
    }
}