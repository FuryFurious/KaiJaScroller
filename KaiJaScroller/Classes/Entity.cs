using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Entity
{
    Sprite sprite;

    ABrain brain;


    public Entity(Sprite s, ABrain b)
    {
        this.sprite = s;
        this.brain = b;

        this.brain.setEntity(this);
    }


    public void update(GameTime gameTime)
    {
        brain.think(gameTime);
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(sprite);
    }

    public void move(float x, float y)
    {
        this.sprite.Position += new Vector2f(x, y);
    }

    public void setSprite(Sprite s)
    {
        this.sprite = s;
    }

    public void setBrain(ABrain b)
    {
        this.brain = b;
        this.brain.setEntity(this);
    }



}

