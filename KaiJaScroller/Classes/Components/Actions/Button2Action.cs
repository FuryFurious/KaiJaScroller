using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Button2Action : IActionListener
{

    public bool performed(GameTime gameTime, Entity source, String name)
    {

        return GameStateManager.input.isClicked(Keyboard.Key.R) || GameStateManager.pad.isClicked(Help.B);
    }
}

