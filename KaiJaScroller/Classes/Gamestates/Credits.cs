using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;



    public class Credits:IGameState
    {
        Text text1 = new Text("Stuff: \n" + "Kai Bornemann", Assets.font1);
        Text text2 = new Text("Things: \n" + "Jarek Brueggemann", Assets.font1);
        float speed;

        public void init()
        {
            speed = 1;
            text1.Position = new Vector2f(200, Settings.windowHeight + 50);
            text2.Position = new Vector2f(200, Settings.windowHeight + 150);
        }

        public EGameState update(GameTime gameTime) 
        {
            
            text1.Position = new Vector2f(200, text1.Position.Y - speed);
            text2.Position = new Vector2f(200, text2.Position.Y - speed);

            if (GameStateManager.pad.isClicked(Help.Start))
            {
                
                return EGameState.MainMenu;
            }
            else return EGameState.Credits;
        }

        public void draw(GameTime gameTime, SFML.Graphics.RenderWindow window)
        {
            window.Clear();
            window.Draw(text1);
            window.Draw(text2);
        }
    }

