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
            this.entity.move(0, -speed);
        }
        else if (InGame.input.isPressed(Keyboard.Key.A))
        {
            this.entity.move(0, speed);
        }

        if (InGame.input.isPressed(Keyboard.Key.S))
        {
            this.entity.move(-speed, 0);
        }
        else if (InGame.input.isPressed(Keyboard.Key.D))
        {
            this.entity.move(speed, 0);
        }
    }
}