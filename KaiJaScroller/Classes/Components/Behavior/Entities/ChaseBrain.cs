using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBrain : ABehavior 
{
    public float speed = 2;
    public float jumpPower = 5;

    public float chaseRangeX = 150;
    public float chaseRangeY = 150;

    float pX;
    float pY;
    float eX;
    float eY;


    public ChaseBrain()
    {
    }

    public override void init()
    {   
    }
    

    public override void update(GameTime gameTime)
    {
        pX = this.entity.ingame.player.boundingBox.CenterX;
        pY = this.entity.ingame.player.boundingBox.CenterY;
        eX = this.entity.boundingBox.CenterX;
        eY = this.entity.boundingBox.CenterY;

        if (Help.distance(pX, eX) < chaseRangeX && Help.distance(pY, eY) < chaseRangeY)
        {
            //moving
            if (pX - eX < -5 && pX - eX > -chaseRangeX && this.entity.canMoveLeft(speed, 0))
            {
                this.entity.moveLeft(speed);
            }
            else if (pX - eX > 5 && pX - eX < chaseRangeX && this.entity.canMoveRight(speed, 0))
            {
                this.entity.moveRight(speed);
            }


            if (pX - eX > 5 && pX - eX < chaseRangeX)
            {

                if (!this.entity.canMoveRight(speed, 0))
                {
                    if (this.entity.canMoveRight(speed, 32) || this.entity.canMoveRight(speed, 64))
                    {
                        this.entity.jump(jumpPower);
                    }
                }

                if (this.entity.canMoveRight(speed, -32) && pX > eX && pY < eY)
                {
                    this.entity.jump(jumpPower);
                }
            }
            if (pX - eX < -5 && pX - eX > -chaseRangeX)
            {

                if (!this.entity.canMoveLeft(speed, 0))
                {
                    if (this.entity.canMoveLeft(speed, 32) || this.entity.canMoveLeft(speed, 64))
                    {

                        this.entity.jump(jumpPower);
                    }
                }

                if (this.entity.canMoveLeft(speed, -32) && pX < eX && pY < eY)
                {
                    this.entity.jump(jumpPower);
                }

            }

        }

    }

    public override void onKill()
    {

    }


}