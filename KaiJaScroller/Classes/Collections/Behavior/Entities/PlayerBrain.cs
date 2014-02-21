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

    //TODO: remove cooldown from button1Action and add elsewhere better
    IAction action1 = new Button1Action();

    public PlayerBrain()
    {

    }

    public override void update(GameTime gameTime)
    {
        
        if (InGame.input.isPressed(Keyboard.Key.A) && this.entity.canMoveLeft(-speed))
        {
            this.entity.moveHorz(-speed);
        }
        else if (InGame.input.isPressed(Keyboard.Key.D) && this.entity.canMoveRight(speed))
        {
            this.entity.moveHorz(speed);
        }

        if (action1.performed(gameTime, this.entity.ingame))
        {

            Entity bull = new Entity();//   new Sprite(Assets.fireballTexture), 
                                        //new SimpleBullet(), 
                                        //new NoPhysics());
            
            Sprite sp = new Sprite(Assets.fireballTexture);
            sp.TextureRect = new IntRect(0, 0, 32, 32);

            bull.setSprite(sp);
            bull.setBrain(new SimpleBullet(this.entity.direction));
            bull.setPhysics(new NoPhysics());

            bull.damage = 99;
            bull.boundingBox = new BoundingBox(this.entity.position.X, this.entity.position.Y, 32, 32);
            bull.setPosition(this.entity.position.X, this.entity.position.Y);
            

            this.entity.ingame.bullets.Add(bull);
        }
         
        float leftX = InGame.pad.getLeftX();
        
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

}