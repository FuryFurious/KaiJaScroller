using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PauseOverlay : IOverlayState
{
    Text title = new Text("Paused", Assets.font1);
    RectangleShape shape;

    Text returnText = new Text("Continue", Assets.font1);
    Text exitText = new Text("Exit", Assets.font1);

    int index = 0;

    public PauseOverlay()
    {
        this.shape = new RectangleShape();
        this.shape.FillColor = new Color(0, 0, 0, 127);
        this.shape.Size = new Vector2f(Settings.windowWidth, Settings.windowHeight);


        returnText.Position = new Vector2f(Settings.windowWidth / 2, Settings.windowHeight / 2);
        exitText.Position = new Vector2f(Settings.windowWidth / 2, 40 + Settings.windowHeight / 2);
    }

    public bool isPaused()
    {
        return true;
    }

    public void init()
    {
       
    }

    public EOverlayState update(GameTime gameTime)
    {
        

        if (GameStateManager.input.isClicked(Keyboard.Key.W) || GameStateManager.input.isClicked(Keyboard.Key.S) || GameStateManager.pad.leftDown() || GameStateManager.pad.leftUp())
            index = (index + 1) % 2;

        if (index == 0)
        {
            returnText.Scale = new Vector2f(1.5f, 1.5f);
            exitText.Scale = new Vector2f(1, 1);

            if (GameStateManager.input.isClicked(Keyboard.Key.Space) || GameStateManager.pad.isClicked(Help.Start))
                return EOverlayState.None;
        }
        else
        {
            exitText.Scale = new Vector2f(1.5f, 1.5f);
            returnText.Scale = new Vector2f(1, 1);

            if (GameStateManager.input.isClicked(Keyboard.Key.Space) || GameStateManager.pad.isClicked(Help.Start))
                return EOverlayState.MainMenu;
        }

        return EOverlayState.Pause;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(shape);
        target.Draw(title);
        target.Draw(returnText);
        target.Draw(exitText);
    }
}