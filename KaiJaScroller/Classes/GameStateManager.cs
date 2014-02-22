using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class GameStateManager : Game
{
    public static Input input;
    public static Gamepad pad;

    EGameState currentGameState = EGameState.InGame;
    EGameState prevGameState;

    IGameState gameState;

    Text fps;

    public GameStateManager()
        : base(Settings.windowWidth, Settings.windowHeight, Settings.WINDOWTITLE, Settings.windowStyles)
    {
        pad = new Gamepad();
        List<Keyboard.Key> keys = new List<Keyboard.Key>();
        keys.Add(Keyboard.Key.W);
        keys.Add(Keyboard.Key.A);
        keys.Add(Keyboard.Key.S);
        keys.Add(Keyboard.Key.D);
        keys.Add(Keyboard.Key.E);
        keys.Add(Keyboard.Key.Q);
        keys.Add(Keyboard.Key.F1);
        keys.Add(Keyboard.Key.Escape);
        keys.Add(Keyboard.Key.Space);

        input = new Input(keys);

        fps = new Text("", Assets.font1);

        handleNewGameState();
    }

    public override void update(GameTime gameTime)
    {
        pad.update();
        input.update();

        currentGameState = gameState.update(gameTime);

        if (currentGameState != prevGameState)
            handleNewGameState();
    }

    public override void draw(GameTime gameTime, RenderWindow window)
    {
        fps.DisplayedString = "" + 1.0f / (float)gameTime.ElapsedTime.TotalSeconds;

        gameState.draw(gameTime, window);

        if(Settings.drawBoundings)
            window.Draw(fps);
    }

    private void handleNewGameState()
    {
        switch (currentGameState)
        {
            case EGameState.MainMenu:
                gameState = new MainMenu();
                break;

            case EGameState.InGame:
                gameState = new InGame();
                break;

            case EGameState.Credits:
                gameState = new Credits();
                break;
            case EGameState.Restart:
                gameState = new InGame();
                currentGameState = EGameState.InGame;
                break;

            default:
                window.Close();
                break;
        }

        gameState.init();
        prevGameState = currentGameState;
    }
}
