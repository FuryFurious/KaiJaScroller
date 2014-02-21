using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Button1Action : IAction
{
    double cooldown = 0.25d;

    public bool performed(GameTime gameTime, InGame ingame)
    {
        if (cooldown > 0)
            cooldown -= gameTime.ElapsedTime.TotalSeconds;

        if (cooldown <= 0 &&(InGame.input.isClicked(Keyboard.Key.E) || InGame.pad.isClicked(Help.X)) )
        {
            cooldown = 0.25d;
            return true;
        }

        return false;
    }
}

