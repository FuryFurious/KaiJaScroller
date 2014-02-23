using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimplePhysic : APhysicComponent
{
    bool isFalling = true;

    public double fallTime = 0;

    float fallSpeed = 0;


    public SimplePhysic()
    {

    }

    new public void jump(float power)
    {
        if (!isFalling)
        {
            resetPhysics();
            isFalling = true;
            fallSpeed = -power;
        }
    }

    public override void update(GameTime gameTime)
    {
        //standing:
        if (!isFalling)
        {
            float boundingBoxBottomY = this.entity.boundingBox.Bottom;

            float boundingBoxBottomX1 = this.entity.boundingBox.Left;
            float boundingBoxBottomX2 = this.entity.boundingBox.Right;

            bool willFall = true;

            foreach (BoundingBox bb in this.entity.ingame.collisionRects)
            {
                if (bb.intersectsHorzLine(boundingBoxBottomY + 1, boundingBoxBottomX1, boundingBoxBottomX2))
                {
                    willFall = false;
                    break;
                }
            }

            if (willFall)
            {
                resetPhysics();
                fallSpeed = 2;
                isFalling = true;
            }
        }

        //jumping or falling:
        else
        {
            fallTime += gameTime.ElapsedTime.TotalSeconds;
            fallSpeed += (float)fallTime;


            //falling:
            if (fallSpeed > 0)
            {

                float y = this.entity.boundingBox.Bottom;
                float x0 = this.entity.boundingBox.Left;
                float x1 = this.entity.boundingBox.Right;

                foreach (BoundingBox bb in this.entity.ingame.collisionRects)
                    if (bb.intersectsHorzLine(y, x0, x1))
                    {
                        
                        if(bb.Top + 20 > this.entity.boundingBox.Bottom)
                            this.entity.position.Y = bb.Y - this.entity.boundingBox.Height;

                        resetPhysics();
                        return;
                    }

                this.entity.moveVert(fallSpeed);
            }
            //jumping
            else 
            {

                float y = this.entity.boundingBox.Top;
                float x0 = this.entity.boundingBox.Left;
                float x1 = this.entity.boundingBox.Right;

                foreach (BoundingBox bb in this.entity.ingame.collisionRects)
                    if (bb.intersectsHorzLine(y, x0, x1))
                    {
                        resetPhysics();
                        isFalling = true;
                        return;
                    }

                this.entity.moveVert(fallSpeed);
            }
           
        }
    }

    private void resetPhysics()
    {
        fallSpeed = 0;
        fallTime = 0;
        isFalling = false;
    }


    public override void init()
    {
       
    }
}
