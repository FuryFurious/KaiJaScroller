using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseJump : IActionListener
{

    public bool performed(GameTime gameTime, Entity source)
    {
        float pX = source.ingame.player.boundingBox.CenterX;
        float eX = source.boundingBox.CenterX;
        

        
        

        if (source.ingame.player.position.Y < source.position.Y)
        {

            if (pX - eX > 5 && pX - eX < 150)
            {
                if (!source.canMoveRight(2,0))
                {
                    if (source.canMoveRight(2, 32) || source.canMoveRight(2, 64))
                    {
                        return true;
                    }
                }
                
            }
            if (pX - eX < -5 && pX - eX > -150)
            {
                if (!source.canMoveLeft(-2,0))
                {
                    if (source.canMoveLeft(-2, 32) || source.canMoveLeft(-2, 64))
                    {
                        return true;
                    }
                }   
            }

        }








        



        return false;
    }
}