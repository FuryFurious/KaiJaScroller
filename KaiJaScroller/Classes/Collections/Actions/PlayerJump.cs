using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerJump : IAction
{

    public bool performed(GameTime gameTime, InGame ingame)
    {
        return InGame.input.isClicked(SFML.Window.Keyboard.Key.Space);
    }
}