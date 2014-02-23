using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    

    public class MainMenu:IGameState
    {
        RectangleShape start;
        RectangleShape credits;
        RectangleShape exit;

        Text startText = new Text("New Game", Assets.font1);
        Text creditsText = new Text("Credits", Assets.font1);
        Text exitText = new Text("Exit Game", Assets.font1);

        int count;

        public void init()
        {
            start = new RectangleShape();
            credits = new RectangleShape();
            exit = new RectangleShape();

            start.Position = new Vector2f(200, 200);
            start.FillColor = Color.Green;
            start.Size = new Vector2f(80, 50);

            credits.Position = new Vector2f(200, 300);
            credits.FillColor = Color.Green;
            credits.Size = new Vector2f(80, 50);

            exit.Position = new Vector2f(200, 400);
            exit.FillColor = Color.Green;
            exit.Size = new Vector2f(80, 50);

            startText.Position = new Vector2f(230, 200);
            startText.CharacterSize = 40;

            creditsText.Position = new Vector2f(230, 300);
            creditsText.CharacterSize = 40;

            exitText.Position = new Vector2f(230, 400);
            exitText.CharacterSize = 40;
            
        }

        public EGameState update(GameTime gameTime)
        {


            if (GameStateManager.pad.leftDown())
                count = (count+ 1) % 3;
            if (GameStateManager.pad.leftUp())
                count = (count + 2) % 3;

            if (count == 0)
            {
                start.Size = new Vector2f(100, 50) * (float)(Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2)) + new Vector2f(95, 40);
                credits.Size = new Vector2f(100,50);
                exit.Size = new Vector2f(100, 50);
                if (GameStateManager.pad.isClicked(Help.A))
                    return EGameState.InGame;
            }

            else if (count == 1)
            {
                credits.Size = new Vector2f(100, 50) * (float)(Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2)) + new Vector2f(95, 40);
                start.Size = new Vector2f(100, 50);
                exit.Size = new Vector2f(100, 50);
                if (GameStateManager.pad.isClicked(Help.A))
                    return EGameState.Credits;
            }

            else if (count == 2)
            {
                exit.Size = new Vector2f(100, 50) * (float)(Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2)) + new Vector2f(95, 40);
                credits.Size = new Vector2f(100, 50);
                start.Size = new Vector2f(100, 50);
                if (GameStateManager.pad.isClicked(Help.A))
                    return EGameState.None;
            }
    

            return EGameState.MainMenu;
        }

        public void draw(GameTime gameTime, SFML.Graphics.RenderWindow window)
        {
            window.Clear();
            window.Draw(start);
            window.Draw(credits);
            window.Draw(exit);

            window.Draw(startText);
            window.Draw(creditsText);
            window.Draw(exitText);
            
        }
    }

