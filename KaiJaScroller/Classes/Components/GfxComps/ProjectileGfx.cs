﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProjectileGfx : AGfxComp
{

    EDirection dir = EDirection.Left;

    public override void init()
    {
       // this.sprite.Transform.        
       // this.sprite.Scale = new SFML.Window.Vector2f(-1, 1);
    }

    public override void update(GameTime gameTime)
    {
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

