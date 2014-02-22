using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IActionListener
{
    bool performed(GameTime gameTime, InGame ingame);
}