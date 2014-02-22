using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Restart:IGameState
    {

        public void init()
        {
            
        }

        public EGameState update(GameTime gameTime)
        {
            return EGameState.InGame;
        }

        public void draw(GameTime gameTime, SFML.Graphics.RenderWindow window)
        {
            
        }
    }
