using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BombBrain : ABehavior, IActionListener
{
    float pX;
    float pY;
    float eX;
    float eY;
    bool isChasing;
    bool canRight;
    Fireball fireball;
    Bomb bomb;

    public float chase = 150;

    public float chaseMiddle1 = 101;
    public float chaseMiddle2 = 99;
    public float lowX = 5;

    public float chaseRangeY = 150;

    public float jumpPower = 5;
    public float speed = 0;


    public BombBrain()
    {

    }

    public override void init()
    {
        bomb = new Bomb();
        bomb.setAction(this);
        bomb.setEntity(this.entity);
    }

    public override void update(GameTime gameTime)
    {
        pX = this.entity.ingame.player.boundingBox.CenterX;
        pY = this.entity.ingame.player.boundingBox.CenterY;
        eX = this.entity.boundingBox.CenterX;
        eY = this.entity.boundingBox.CenterY;

        //moving
        if (pX > eX)
        {
            this.entity.moveRight(speed);
        }
        else if (pX < eX)
        {
            this.entity.moveLeft(speed);
        }

        bomb.update(gameTime);

    }

    public override void onKill()
    {

    }

    public bool willFollow()
    {
        return true;
    }

    public bool performed(GameTime gameTime, Entity source, String name)
    {
       

         if (name == bomb.name)
        {
            
            
                bomb.xSpeed = (float)Help.random.NextDouble() * 5.0f;
            

            //if(Math.Abs(pX - eX) < 100)
            return true;
        }

        return false;
    }
}