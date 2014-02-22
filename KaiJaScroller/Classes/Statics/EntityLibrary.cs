using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class EntityLibrary
{
    public static Entity getEntity(EEntityType type)
    {
        Entity e = new Entity();
        e.isFriendly = false;
        BoundingBox bb = null;
        Sprite sprite = null;
        AGfxComp gfxComp = null;
        ABehavior behavior = null;
        APhysicComponent physics = null;

        switch (type)
        {
            case EEntityType.Player:

                e.isFriendly = true;
                bb = new BoundingBox(0, 0, 16, 32);
                bb.offsetX = 8;

                sprite = new Sprite(Assets.zombieTexture);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new ProjectileGfx();

                behavior = new PlayerBrain();
                physics = new SimplePhysic(new PlayerJump());

                break;

            case EEntityType.Imp:

                bb = new BoundingBox(0, 0, 16, 32);
                bb.offsetX = 8;

                e.damage = 1;

                sprite = new Sprite(Assets.impTexture);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new ProjectileGfx();

                behavior = new ChaseBrain();
                physics = new SimplePhysic(new ChaseJump());
                break;

        }

        gfxComp.setSprite(sprite);
        e.setPhysics(physics);
        e.setBrain(behavior);
        e.boundingBox = bb;
        e.setGfxComp(gfxComp);
     
        return e;
    }

    public static Particle getParticle( EParticleType type, Vector2f start)
    {
        switch (type)
        {
            case EParticleType.Smoke:

                Particle p = new Particle();

                Sprite s1 = new Sprite(Assets.smokeParticle);
               // s1.Color = Color.Black;
                s1.Origin = new Vector2f(16, 16);
                s1.Rotation = (float)(Help.random.NextDouble() * 360.0);

                p.setSprite(s1);
                p.setPosition(start);
                p.setBehavior(new Smoke());


                return p;
        }
        return null;
    }
}

