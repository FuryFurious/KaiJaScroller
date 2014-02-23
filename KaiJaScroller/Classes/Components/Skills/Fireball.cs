using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Fireball : ASkill
{


    public Fireball()
    {
        this.name = "Fireball";

        this.maxCooldown = 0.25;
        this.curCooldown = 0.25;
    }

    public override void onUse(GameTime gameTime)
    {
        Entity bull = new Entity();

        Sprite sp = new Sprite(Assets.magicBall);
        sp.TextureRect = new IntRect(0, 0, 32, 32);
        AGfxComp comp = new ProjectileGfx();
        comp.setSprite(sp);

        bull.setGfxComp(comp);
        bull.setBrain(new SimpleBullet(this.entity.direction));
        bull.setPhysics(new NoPhysics());


        Vector2f startPos = this.entity.boundingBox.Center - new Vector2f(32,16);

        bull.damage = 99;
        bull.boundingBox = new BoundingBox(startPos.X, startPos.Y, 16, 16);
        bull.boundingBox.offsetX = 8;
        bull.boundingBox.offsetY = 8;

        bull.setPosition(startPos.X, startPos.Y);

        this.entity.ingame.addBullet(bull, this.entity.isFriendly);
    }
}

