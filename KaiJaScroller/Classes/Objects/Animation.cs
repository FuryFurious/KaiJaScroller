using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Animation
{
    public Entity entity;

    public double time;
    public double animationTime;

    public int numPics;

    public Animation()
    {

    }

    public void update(GameTime gameTime)
    {
        if (time < animationTime)
        {

            time += gameTime.ElapsedTime.TotalSeconds;

            double delta = time / animationTime;

            int currentPic = (int)(delta * numPics);

            if (currentPic >= numPics)
                currentPic = numPics - 1;

            entity.gfxComp.sprite.TextureRect = new SFML.Graphics.IntRect(currentPic * 32, 0, 32, 32);
        }
        else if (time >= animationTime)
        {
            entity.gfxComp.sprite.TextureRect = new SFML.Graphics.IntRect(0, 0, 32, 32);

        }
    }

    public void start()
    {
        time = 0;
    }
}
