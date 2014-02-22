using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerJump : IAction
{

    public bool performed(GameTime gameTime, InGame ingame)
    {
        return GameStateManager.input.isClicked(Keyboard.Key.Space) || GameStateManager.pad.isClicked(Help.A);
    }
}