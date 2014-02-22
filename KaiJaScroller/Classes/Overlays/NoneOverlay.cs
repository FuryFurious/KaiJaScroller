using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NoneOverlay : IOverlayState
{

    public bool isPaused()
    {
        return false;
    }

    public void init()
    {
       
    }

    public EOverlayState update(GameTime gameTime)
    {
        if (GameStateManager.pad.isClicked(Help.LB) || GameStateManager.input.isClicked(Keyboard.Key.Escape))
            return EOverlayState.Pause;


        return EOverlayState.None;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
      
    }
}