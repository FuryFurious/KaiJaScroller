using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NoAction : IActionListener
{
    public bool performed(GameTime gameTime, InGame ingame)
    {
        return false;
    }
}