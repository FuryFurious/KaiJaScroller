using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimpleBomb : ABehavior
{
    float xSpeed = 5.0f;
    float ySpeed = -3.5f;

    public SimpleBomb(EDirection direction)
    {
        if (direction == EDirection.Left)
            xSpeed *= -1;
    }

    public override void init()
    {
        
    }

    public override void update(GameTime gameTime)
    {

        if (!this.entity.canMoveLeft(-3) || !this.entity.canMoveRight(3) || !this.entity.canMoveUp(-3) || !this.entity.canMoveDown(3))
            this.entity.exists = false;

        this.entity.moveLeft(xSpeed);

        this.entity.moveVert(ySpeed);
    }

    public override void onKill()
    {
        Vector2f startPos = this.entity.boundingBox.Center;
        this.entity.ingame.particles.Add(EntityLibrary.getParticle(EParticleType.Smoke, startPos));
        this.entity.ingame.particles.Add(EntityLibrary.getParticle(EParticleType.Smoke, startPos));
        this.entity.ingame.particles.Add(EntityLibrary.getParticle(EParticleType.Smoke, startPos));


    }
}