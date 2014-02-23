using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimpleBomb : ABehavior
{
    public float xSpeed = 5.0f;
    public float ySpeed = -3.5f;

    EDirection direction;

    public SimpleBomb(EDirection direction)
    {
        this.direction = direction;
    }

    public override void init()
    {
        
    }

    public override void update(GameTime gameTime)
    {

        if (!this.entity.canMoveLeft(0, 0) || !this.entity.canMoveRight(0, 0) || !this.entity.canMoveUp(3) || !this.entity.canMoveDown(3))
        {
            this.entity.exists = false;
            return;
        }

        if(direction == EDirection.Left)
            this.entity.moveLeft(xSpeed);

        else if(direction == EDirection.Right)
            this.entity.moveRight(xSpeed);

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