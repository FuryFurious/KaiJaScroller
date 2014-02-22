using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseJump : IActionListener
{

    public bool performed(GameTime gameTime, Entity source)
    {
        return source.ingame.player.position.Y < source.position.Y;
    }
}