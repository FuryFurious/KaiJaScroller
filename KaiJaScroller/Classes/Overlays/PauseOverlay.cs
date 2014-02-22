using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PauseOverlay : IOverlayState
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
        if (GameStateManager.pad.isClicked(Help.Start) || GameStateManager.input.isClicked(Keyboard.Key.Escape))
            return EOverlayState.None;

        return EOverlayState.Pause;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
       
    }
}