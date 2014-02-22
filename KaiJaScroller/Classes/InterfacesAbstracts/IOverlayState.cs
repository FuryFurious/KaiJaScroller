using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IOverlayState
{

    bool isPaused();

    void init();

    EOverlayState update(GameTime gameTime);

    void draw(GameTime gameTime, RenderTarget target);

}

