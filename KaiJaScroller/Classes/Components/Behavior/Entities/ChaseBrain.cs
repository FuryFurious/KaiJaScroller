using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBrain : ABehavior
{
    float pX;
    float pY;
    float eX;
    float eY;
    bool canLeft;
    bool canRight;
    

    public ChaseBrain()
    {
        
    }

    public override void init()
    {
        //ASkill skill1 = new Fireball();
        //skill1.setAction(new RandomAction(0.5));
        //skill1.setEntity(this.entity);

        //skills.Add(skill1);
        canLeft = false;
        canRight = false;
        
    }

    public override void update(GameTime gameTime)
    {
        pX = this.entity.ingame.player.boundingBox.CenterX;
        pY = this.entity.ingame.player.boundingBox.CenterY;
        eX = this.entity.boundingBox.CenterX;
        eY = this.entity.boundingBox.CenterY;

        //moving
        if (pX - eX < -5 && pX - eX > -150 && this.entity.canMoveLeft(2, 0))
        {
            canLeft = true;
            this.entity.moveLeft(2);
        }
        else if (pX - eX > 5 && pX - eX < 150 && this.entity.canMoveRight(2, 0))
        {
            canRight = true;
            this.entity.moveRight(2);
        }
        Console.Clear();

        //jumping  
        if (pX - eX > 5 && pX - eX < 150)
        {
            if (true)
            {
                if (!this.entity.canMoveRight(2, 0))
                {
                    if (this.entity.canMoveRight(2, 32) || this.entity.canMoveRight(2, 64))
                    {
                        Console.WriteLine("Wrong");
                        Console.WriteLine("Wrong");
                        Console.WriteLine("Wrong");
                        Console.WriteLine("Wrong");
                        this.entity.jump(5);
                    }
                }
            }
            if (this.entity.canMoveRight(2, -32) && pX > eX && pY < eY)
            {
                Console.WriteLine("Right");
                this.entity.jump(5);
            }
        }
        if (pX - eX < -5 && pX - eX > -150)
        {
            if (true)
            {
                if (!this.entity.canMoveLeft(2, 0))
                {
                    if (this.entity.canMoveLeft(2, 32) || this.entity.canMoveLeft(2, 64))
                    {
                        Console.WriteLine("Wrong");
                        Console.WriteLine("Wrong");
                        Console.WriteLine("Wrong");

                        this.entity.jump(5);
                    }
                }
            }
            if (this.entity.canMoveLeft(2, -32) && pX < eX && pY < eY )
            {
                Console.WriteLine("Right");
                this.entity.jump(5);
            }
            
        }



        updateSkills(gameTime);
    }

    public override void onKill()
    {

    }
}