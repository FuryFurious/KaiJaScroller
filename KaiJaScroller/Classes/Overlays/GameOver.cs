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
        if (GameStateManager.input.isClicked(SFML.Window.Keyboard.Key.Escape))
            return EOverlayState.Restart;

        return EOverlayState.GameOver;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
       
    }
}

