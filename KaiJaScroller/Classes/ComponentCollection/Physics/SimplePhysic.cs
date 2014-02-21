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
        float boundingBoxBottomY = this.entity.boundingBox.Top + this.entity.boundingBox.Height;
        float boundingBoxBottomX = this.entity.boundingBox.Left;



        if (!isFalling)
        {
            bool willFall = true;

            foreach (FloatRect r in ingame.collisionRects)
            {
                if (r.Contains(boundingBoxBottomX, boundingBoxBottomY))
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

            if (InGame.input.isClicked(Keyboard.Key.Space) || InGame.pad.isClicked(Help.A))
            {
                resetPhysics();
                isFalling = true;
                fallSpeed = -5;
            }
        }


        else
        {
            fallTime += gameTime.ElapsedTime.TotalSeconds;
            fallSpeed += (float)fallTime;

            this.entity.move(0, fallSpeed);

            foreach(FloatRect r in ingame.collisionRects)
                if(this.entity.boundingBox.Top <= r.Top)
                    if (this.entity.boundingBox.Intersects(r))
                    {
                        resetPhysics();
                        this.entity.position.Y = r.Top - this.entity.boundingBox.Height;
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
