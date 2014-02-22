using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBrain : ABehavior
{
    public ChaseBrain()
    {
        
    }

    public override void init()
    {
        
    }

    public override void update(GameTime gameTime)
    {
        if (this.entity.ingame.player.position.X - this.entity.position.X < 5  && this.entity.canMoveLeft(-5))
            this.entity.moveHorz(-5);

        else if (this.entity.ingame.player.position.X - this.entity.position.X > 5  && this.entity.canMoveRight(5))
            this.entity.moveHorz(5);
    }

    public override void onKill()
    {
        
    }
}