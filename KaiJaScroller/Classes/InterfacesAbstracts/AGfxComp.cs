using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class AGfxComp
{

    protected Entity entity;
    public Sprite sprite;

    public float xOffset = 0;
    public float yOffset = 0;

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

    public void mirror()
    {

        if (this.entity.direction == EDirection.Left)
        {
            this.sprite.Scale = new Vector2f(1, 1);
            this.xOffset = 0;
        }
        else if(this.entity.direction == EDirection.Right)
        {
            this.sprite.Scale = new Vector2f(-1, 1);
            this.xOffset = 32;
        }
        
    }
}

