using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Particle
{
    public Sprite sprite;
    private AParticleBehavior behavior;

    public double lifeTime;

    public Particle(double lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public void setSprite(Sprite s)
    {
        sprite = s;
    }

    public void setPosition(Vector2f pos)
    {
        this.sprite.Position = pos;
    }

    public void setBehavior(AParticleBehavior behavior)
    {
        this.behavior = behavior;

        this.behavior.parent = this;

        this.behavior.init();
    }

    public void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        behavior.update(gameTime);
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(sprite);
    }

}

