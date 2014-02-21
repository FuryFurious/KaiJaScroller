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

        RectangleShape current;

         

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

            startText.Position = new Vector2f(230, 220);

            creditsText.Position = new Vector2f(230, 320);

            exitText.Position = new Vector2f(230, 420);

            current = start;

            
        }

        public EGameState update(GameTime gameTime)
        {

            return EGameState.MainMenu;
        }

        public void draw(GameTime gameTime, SFML.Graphics.RenderWindow window)
        {
            window.Draw(start);
            window.Draw(credits);
            window.Draw(exit);

            window.Draw(startText);
            window.Draw(creditsText);
            window.Draw(exitText);



            
        }
    }

