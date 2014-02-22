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
}

