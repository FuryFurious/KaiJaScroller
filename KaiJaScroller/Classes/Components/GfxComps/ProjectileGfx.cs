using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProjectileGfx : AGfxComp
{

    public override void init()
    {
      
    }

    public override void update(GameTime gameTime)
    {
        this.sprite.Position = this.entity.position;
    }

    public override void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(this.sprite);
    }
}

