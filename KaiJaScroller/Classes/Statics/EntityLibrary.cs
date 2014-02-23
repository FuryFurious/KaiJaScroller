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

                gfxComp = new EntityGfx();
                behavior = new PlayerBrain();
                physics = new SimplePhysic();

                break;

            case EEntityType.Imp:

                bb = new BoundingBox(0, 0, 16, 32);
                bb.offsetX = 8;



                e.damage = 0;

                sprite = new Sprite(Assets.impTexture);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new EntityGfx();

                behavior =new ChaseBombBrain();
                physics = new SimplePhysic();
                break;

            case EEntityType.Turret:

                bb = new BoundingBox(0, 0, 32, 32);
                bb.offsetX = 0;

                e.damage = 0;
                e.hitpoints[0] = int.MaxValue;

                sprite = new Sprite(Assets.turretTexture);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new EntityGfx();

                behavior = new TurretBrain();
                physics = new NoPhysics();
                break;


            case EEntityType.Rogue:

                bb = new BoundingBox(0, 0, 16, 32);
                bb.offsetX = 8;

                e.damage = 0;
                e.hitpoints[0] = 1;

                sprite = new Sprite(Assets.rogue);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new EntityGfx();

                behavior = new RogueBrain();

                physics = new SimplePhysic();

                break;

            case EEntityType.Mage:

                bb = new BoundingBox(0, 0, 16, 32);
                bb.offsetX = 8;

                e.damage = 0;
                e.hitpoints[0] = 1;

                sprite = new Sprite(Assets.mage);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new EntityGfx();

                behavior = new ChaseBrain();
                physics = new SimplePhysic();

                break;

            case EEntityType.Bomberman:

                bb = new BoundingBox(0, 0, 16, 32);
                bb.offsetX = 8;

                e.damage = 0;
                e.hitpoints[0] = 1;

                sprite = new Sprite(Assets.bomberman);
                sprite.TextureRect = new IntRect(0, 0, 32, 32);

                gfxComp = new EntityGfx();

                behavior = new BombBrain();
                physics = new SimplePhysic();

                break;
        }

        gfxComp.setSprite(sprite);
        e.setPhysics(physics);
        e.setBrain(behavior);
        e.boundingBox = bb;
        e.setGfxComp(gfxComp);

        //gfxComp.init();
        //physics.init();
      //  behavior.init();

        return e;
    }

    public static Particle getParticle( EParticleType type, Vector2f start)
    {
        Particle p = new Particle();
        Sprite s1 = null;

        switch (type)
        {
            case EParticleType.Smoke:


                s1 = new Sprite(Assets.smokeParticle);
                s1.Origin = new Vector2f(16, 16);
                s1.Rotation = (float)(Help.random.NextDouble() * 360.0);

                p.setSprite(s1);
                p.setPosition(start);
                p.setBehavior(new Smoke());


                break;

            case EParticleType.Blood:

                s1 = new Sprite(Assets.bloodParticle);
                s1.TextureRect = new IntRect(Help.random.Next(3) * 32, 0, 32, 32);
                s1.Color = new Color(255, 0, 0);
                s1.Origin = new Vector2f(16, 16);
                s1.Rotation = (float)(Help.random.NextDouble() * 360);

                float randomHelp = (float)Help.random.NextDouble()*1.5f;
                randomHelp = (float)Math.Max(randomHelp, 0.5);

                s1.Scale = new Vector2f(randomHelp,randomHelp);

                p.setSprite(s1);
                p.setPosition(start);
                p.setBehavior(new Fade(Math.Max(3*Help.random.NextDouble(), 0.5)));

                break;
        }
     //   p.setPosition(start);
        return p;
    }
}

