using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimpleSword : ABehavior
{
    public float xSpeed = 5.0f;
    public double lifeTime = 0.5;
    public float count = 0.5f;
    public float reach = 12;

    public Entity e;
    
    

  //  EDirection direction;

    public SimpleSword(EDirection direction)
    {
    //    this.direction = direction;
    }

    public override void init()
    {
      //  if (this.entity.direction == EDirection.Left)
     //       this.entity.gfxComp.mirror();

        //this.entity.direction = EDirection.Right;
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
            //this.entity.direction = e.direction;
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