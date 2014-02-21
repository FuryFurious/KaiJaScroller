using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InGame : IGameState
{
    public void init()
    {
    
    }

    public EGameState update(GameTime gameTime)
    {
        return EGameState.InGame;
    }

    public void draw(GameTime gameTime, RenderWindow window)
    {
       
    }
}