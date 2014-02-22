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
        switch (type)
        {
            case EEntityType.Player:

                Entity player = new Entity();

                player.setPhysics(new SimplePhysic(new PlayerJump()));
                player.boundingBox = new BoundingBox(0, 0, 16, 32);
                player.boundingBox.offsetX = 8;

                Sprite playerSprite = new Sprite(Assets.zombieTexture);
                playerSprite.TextureRect = new IntRect(0, 0, 32, 32);

                player.setSprite(playerSprite);

                PlayerBrain brain = new PlayerBrain();
                player.setBrain(brain);
                return player;


            case EEntityType.Imp:

                Entity enemy1 = new Entity();
                enemy1.isFriendly = false;

                Sprite s = new Sprite(Assets.impTexture);
                s.TextureRect = new IntRect(0, 0, 32, 32);
                ABehavior enemyBehave = new ChaseBrain();

                enemy1.setSprite(s);
                enemy1.setBrain(enemyBehave);
                enemy1.setPhysics(new SimplePhysic(new ChaseJump()));

                enemy1.hitpoints[1] = 10;
                enemy1.damage = 3;
                enemy1.boundingBox = new BoundingBox(0, 0, 32, 32);

                return enemy1;

        }

        return null;

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

