using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerBrain : ABrain
{
    float speed = 3;

    public PlayerBrain()
    {

    }

    public override void think(GameTime gameTime)
    {
        
        if (InGame.input.isPressed(Keyboard.Key.W))
        {
            this.entity.moveVert( -speed);
        }
        else if (InGame.input.isPressed(Keyboard.Key.S))
        {
            this.entity.moveVert( speed);
        }

        if (InGame.input.isPressed(Keyboard.Key.A) && this.entity.canMoveLeft(-speed))
        {
            this.entity.moveHorz(-speed);
        }
        else if (InGame.input.isPressed(Keyboard.Key.D) && this.entity.canMoveRight(speed))
        {
            this.entity.moveHorz(speed);
        }
         
        float leftX = InGame.pad.getLeftX();
        
        if (leftX > 0)
            leftX = ((Help.Clamp(leftX, 5, 95) - 5.0f )/90.0f);

        else if (leftX < 0)
            leftX = ((Help.Clamp(leftX, -95, -5) + 5.0f) / 90.0f);


        if (Math.Abs(leftX) > 0.2f && this.entity.canMoveLeft(-speed))
        this.entity.moveHorz(leftX * speed); 
    }
}