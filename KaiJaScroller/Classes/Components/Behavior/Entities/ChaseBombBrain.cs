using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBombBrain : ABehavior, IActionListener
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
    
    public float chaseMiddle1   = 101;
    public float chaseMiddle2   = 99;
    public float lowX           = 5;
    
    public float chaseRangeY    = 150;

    public float jumpPower      = 5;
    public float speed          = 2;


    public ChaseBombBrain()
    {

    }

    public override void init()
    {
        fireball = new Fireball();
        fireball.setAction(this);
        fireball.setEntity(this.entity);
     
        isChasing = false;
        

      
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

        isChasing = false;

        if ((pX - eX >= chaseMiddle1 && pX - eX < chase) || (pX - eX <= -chaseMiddle1 && pX - eX > -chase))
        {
            isChasing = true;
        }

        

        //moving
        if (this.entity.canMoveRight(speed, 0) && ((pX - eX < -lowX && pX - eX > -chaseMiddle2 || (pX - eX >= chaseMiddle1 && pX - eX < chase))))
        {
            this.entity.moveRight(speed);
        }
        if (this.entity.canMoveLeft(speed, 0) && ((pX - eX > lowX && pX - eX < chaseMiddle2 || (pX - eX <= -chaseMiddle1 && pX - eX > -chase))))
        {
            this.entity.moveLeft(speed);
        }


        //     fireball.update(gameTime);

              bomb.update(gameTime);

        
        //jumping 

              if (pX - eX < -lowX && pX - eX > -chaseMiddle2 || (pX - eX >= chaseMiddle1 && pX - eX < chase))
        {
            if (true)
            {
                /*if (!this.entity.canMoveRight(2, 0))
                {
                    if (this.entity.canMoveRight(2, 32) || this.entity.canMoveRight(2, 64))
                    {
                        this.entity.jump(5);
                    }
                }*/
            }
            

            if (this.entity.canMoveRight(speed, -32))
            {
                this.entity.jump(jumpPower);
            }
        }
        if ((pX - eX > lowX && pX - eX < chaseMiddle2 || (pX - eX <= -chaseMiddle1 && pX - eX > -chase)))
        {
            if (true)
            {
                /*if (!this.entity.canMoveLeft(2, 0))
                {
                    if (this.entity.canMoveLeft(2, 32) || this.entity.canMoveLeft(2, 64))
                    {

                        this.entity.jump(5);
                    }
                }*/
            }
            if (this.entity.canMoveLeft(speed, -32))
            {
                this.entity.jump(jumpPower);
            }

            
        }
         
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
        if (name == fireball.name)
        {
            return true;
        }

        else if (name == bomb.name)
        {
          //  Console.WriteLine(isChasing);
            if (isChasing)
            bomb.xSpeed = +(float)Help.random.NextDouble() * 10.0f;
            else
            {
                bomb.xSpeed = -(float)Help.random.NextDouble() * 10.0f;
            }
            
            //if(Math.Abs(pX - eX) < 100)
            return true;
        }

        return false;
    }
}