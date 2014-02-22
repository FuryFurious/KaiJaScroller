using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameOver : IOverlayState
{
    public bool isPaused()
    {
        return true;
    }

    public void init()
    {
       
    }

    public EOverlayState update(GameTime gameTime)
    {
        return EOverlayState.GameOver;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
       
    }
}

