using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



interface IGameState
{

    void init();
    EGameState update(GameTime gameTime);
    void draw(GameTime gameTime, RenderWindow window);
}

