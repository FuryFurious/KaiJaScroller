using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class AGfxComp
{
    Entity entity;
    Sprite sprite;

    public abstract void init();

    public abstract void update(GameTime gameTime);

    public abstract void draw(GameTime gameTime, RenderTarget target);

    public void setEntity(Entity e)
    {
        this.entity = e;
    }

    public void setSprite(Sprite s)
    {
        this.sprite = s;
    }
}

