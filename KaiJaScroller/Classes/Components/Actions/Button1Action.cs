﻿using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Button1Action : IActionListener
{

    public bool performed(GameTime gameTime, Entity source)
    {
       return GameStateManager.input.isClicked(Keyboard.Key.E) || GameStateManager.pad.isClicked(Help.X);
    }
}

