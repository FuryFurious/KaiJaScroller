using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimplePhysic : PhysicComponent
{
    bool isFalling = true;

    double fallTime = 0;
    float fallSpeed = 0;

    public SimplePhysic()
    {

    }

    public override void update(GameTime gameTime, InGame ingame)
    {

        //standing:
        if (!isFalling)
        {
            float boundingBoxBottomY = this.entity.boundingBox.Bottom;

            float boundingBoxBottomX1 = this.entity.boundingBox.Left;
            float boundingBoxBottomX2 = this.entity.boundingBox.Right;

            bool willFall = true;

            foreach (BoundingBox bb in ingame.collisionRects)
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

            if (InGame.input.isClicked(Keyboard.Key.Space))
            {
                resetPhysics();
                isFalling = true;
                fallSpeed = -5;
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

                foreach (BoundingBox bb in ingame.collisionRects)
                    if (bb.intersectsHorzLine(y, x0, x1))
                    {
                        resetPhysics();
                        this.entity.position.Y = bb.Y - this.entity.boundingBox.Height;
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

                foreach (BoundingBox bb in ingame.collisionRects)
                    if (bb.intersectsHorzLine(y, x0, x1))
                    {
                        resetPhysics();
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

}
