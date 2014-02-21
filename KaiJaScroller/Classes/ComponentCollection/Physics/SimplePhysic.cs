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

    public SimplePhysic()
    {

    }

    public override void update(GameTime gameTime, InGame ingame)
    {

        if (InGame.input.isClicked(Keyboard.Key.Space))
            isFalling = true;

        if (isFalling)
        {
            fallTime += gameTime.ElapsedTime.TotalSeconds;

            this.entity.move(0, 3 * (float)fallTime);

            //TODO: later better:
            if(this.entity.boundingBox.Top <= ingame.boundingBox.Top)
                if (this.entity.boundingBox.Intersects(ingame.boundingBox))
                {
                    isFalling = false;
                    this.entity.position.Y = ingame.boundingBox.Top - this.entity.boundingBox.Height;
                }

            Console.WriteLine("falling");
        }
        else
            Console.WriteLine("Notfalling");
    }

}
