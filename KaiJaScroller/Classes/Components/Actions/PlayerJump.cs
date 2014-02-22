using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerJump : IActionListener
{

    public bool performed(GameTime gameTime, Entity source)
    {
        return GameStateManager.input.isClicked(Keyboard.Key.Space) || GameStateManager.pad.isClicked(Help.A);
    }
}