using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class GameStateManager : Game
{

    EGameState currentGameState = EGameState.InGame;
    EGameState prevGameState;

    IGameState gameState;

    public GameStateManager()
        : base(Settings.windowWidth, Settings.windowHeight, Settings.WINDOWTITLE, Settings.windowStyles)
    {

        handleNewGameState();
    }

    public override void update(GameTime gameTime)
    {
        currentGameState = gameState.update(gameTime);

        if (currentGameState != prevGameState)
            handleNewGameState();
    }

    public override void draw(GameTime gameTime, RenderWindow window)
    {
        window.Clear(CornflowerBlue);
    }

    private void handleNewGameState()
    {
        switch (currentGameState)
        {
            case EGameState.MainMenu:
                break;

            case EGameState.InGame:
                gameState = new InGame();
                break;

            default:
                window.Close();
                break;
        }

        gameState.init();
        prevGameState = currentGameState;
    }
}
