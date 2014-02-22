using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


public class GameOver : IOverlayState
{
    Sprite screen;

    Text text1 = new Text("You Died", Assets.font1, 90);

    public bool isPaused()
    {
        return true;
    }

    public void init()
    {
        screen = new Sprite(Assets.gameOver);
        screen.Position = new Vector2f(0, 0);

        text1.Position = new Vector2f(285, 350);
        text1.Color = Color.Red;
        
        
       
    }

    public EOverlayState update(GameTime gameTime)
    {
        if (GameStateManager.input.isClicked(SFML.Window.Keyboard.Key.Escape) || GameStateManager.pad.isClicked(Help.Start))
            return EOverlayState.Restart;

        return EOverlayState.GameOver;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(screen);
        target.Draw(text1);
    }
}

