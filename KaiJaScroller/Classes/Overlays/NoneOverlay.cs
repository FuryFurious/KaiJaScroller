using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NoneOverlay : IOverlayState
{

    Entity player;

    public NoneOverlay(Entity player)
    {
        this.player = player;
    }

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

        if (player.hitpoints[0] <= 0)
            return EOverlayState.GameOver;


        return EOverlayState.None;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
      
    }
}