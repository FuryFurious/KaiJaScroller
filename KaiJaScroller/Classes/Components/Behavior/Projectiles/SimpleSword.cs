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
    
    

    EDirection direction;

    public SimpleSword(EDirection direction)
    {
        this.direction = direction;

        
    }

    public override void init()
    {
        if (this.entity.direction == EDirection.Left)
            this.entity.gfxComp.mirror();

    }

    public override void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        if (count < reach)
        {
            count += 2f;
        }

        if (direction == EDirection.Left)
                 this.entity.setPosition(this.entity.ingame.player.boundingBox.CenterX - 20 - count, this.entity.ingame.player.boundingBox.CenterY - 13);

        else if (direction == EDirection.Right)
                this.entity.setPosition(this.entity.ingame.player.boundingBox.CenterX - 12 + count, this.entity.ingame.player.boundingBox.CenterY - 13);
        
        if (lifeTime <= 0)
            this.entity.exists = false;
    }

    public override void onKill()
    {
        
    }
}