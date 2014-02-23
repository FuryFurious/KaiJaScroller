using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EntityGfx : AGfxComp
{

    EDirection dir;

    public override void init()
    {
        dir = this.entity.direction;

      
    }

    public override void update(GameTime gameTime)
    {


        if (this.entity.inviTime > 0)
            this.sprite.Color = Color.Red;

        else
            this.sprite.Color = Color.White;

        if (dir != this.entity.direction)
        {
            mirror();
            this.dir = this.entity.direction;
        }


        this.sprite.Position = this.entity.position + new Vector2f(xOffset, yOffset);
    }

    public override void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(this.sprite);
    }



}
