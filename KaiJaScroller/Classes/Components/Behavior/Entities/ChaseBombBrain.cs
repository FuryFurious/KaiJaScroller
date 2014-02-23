﻿using System;
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

    public ChaseBombBrain()
    {

    }

    public override void init()
    {
        fireball = new Fireball();
        fireball.setAction(this);
        fireball.setEntity(this.entity);
        //ASkill skill1 = new Fireball();
        //skill1.setAction(new RandomAction(0.5));
        //skill1.setEntity(this.entity);

        //skills.Add(skill1);
        isChasing = false;
        canRight = false;

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

        if ((pX - eX >= 101 && pX - eX < 150) || (pX - eX <= -101 && pX - eX > -150))
        {
            isChasing = true;
        }

        

        //moving
        if (this.entity.canMoveRight(2, 0) && ((pX - eX < -5 && pX - eX > -99 || (pX - eX >= 101 && pX - eX < 150))))
        {
            this.entity.moveRight(2);
        }
        if (this.entity.canMoveLeft(2, 0) && ((pX - eX > 5 && pX - eX < 99 || (pX - eX <= -101 && pX - eX > -150))))
        {
            this.entity.moveLeft(2);
        }
        Console.Clear();

        //     fireball.update(gameTime);

              bomb.update(gameTime);

        
        //jumping 

              if (pX - eX < -5 && pX - eX > -99 || (pX - eX >= 101 && pX - eX < 150))
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
            

            if (this.entity.canMoveRight(2, -32))
            {
                this.entity.jump(5);
            }
        }
        if ((pX - eX > 5 && pX - eX < 99 || (pX - eX <= -101 && pX - eX > -150)))
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
            if (this.entity.canMoveLeft(2, -32))
            {
                this.entity.jump(5);
            }

            
        }
         
    }

    public override void onKill()
    {

    }

    public bool performed(GameTime gameTime, Entity source, String name)
    {
        if (name == fireball.name)
        {
            return true;
        }

        else if (name == bomb.name)
        {
            Console.WriteLine(isChasing);
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