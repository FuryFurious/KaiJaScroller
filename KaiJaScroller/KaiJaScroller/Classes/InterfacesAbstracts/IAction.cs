using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IAction
{
    bool performed(GameTime gameTime, InGame ingame);
}