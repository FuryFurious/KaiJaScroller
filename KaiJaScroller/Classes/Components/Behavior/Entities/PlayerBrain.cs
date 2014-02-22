using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerBrain : ABehavior
{
    float speed = 3;

    ASkill skill1;

    public PlayerBrain()
    {
    }

    public override void update(GameTime gameTime)
    {

        if (GameStateManager.input.isPressed(Keyboard.Key.A) && this.entity.canMoveLeft(-speed))
        {
            this.entity.moveHorz(-speed);
        }
        else if (GameStateManager.input.isPressed(Keyboard.Key.D) && this.entity.canMoveRight(speed))
        {
            this.entity.moveHorz(speed);
        }

        skill1.update(gameTime);

        float leftX = GameStateManager.pad.getLeftX();
        
        if (leftX > 0)
            leftX = ((Help.Clamp(leftX, 5, 95) - 5.0f ) / 90.0f);

        else if (leftX < 0)
            leftX = ((Help.Clamp(leftX, -95, -5) + 5.0f) / 90.0f);


        if (Math.Abs(leftX) > 0.2f)
        {
            float xHelp = leftX * speed;

            if (xHelp > 0 && this.entity.canMoveRight(xHelp))
                this.entity.moveHorz(xHelp);

            else if (xHelp < 0 && this.entity.canMoveLeft(xHelp))
                this.entity.moveHorz(xHelp);
        }
    }

    public override void onKill()
    {

    }


    public override void init()
    {
        skill1 = new Fireball();
        skill1.setAction(new Button1Action());
        skill1.setEntity(this.entity);
    }
}