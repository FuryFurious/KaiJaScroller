using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Bomb : ASkill
{

    public float xSpeed = 5.0f;
    public float ySpeed = -3.5f;

    public Bomb()
    {
        this.name = "Bomb";

        this.maxCooldown = 0.25;
        this.curCooldown = 0.25;
    }

    public override void onUse(GameTime gameTime)
    {
        Entity bull = new Entity();

        Sprite sp = new Sprite(Assets.bomb);
        sp.TextureRect = new IntRect(0, 0, 32, 32);

        AGfxComp comp = new ProjectileGfx();
        comp.setSprite(sp);


        SimpleBomb b = new SimpleBomb(this.entity.direction);
        b.xSpeed = xSpeed;
        b.ySpeed = ySpeed;
        bull.setGfxComp(comp);
        bull.setBrain(b);
        bull.setPhysics(new SimplePhysic());
     //   bull.setPhysics(new NoPhysics());
        Vector2f startPos = this.entity.boundingBox.Center;

        bull.damage = 99;
        bull.boundingBox = new BoundingBox(startPos.X, startPos.Y, 8, 8);
        bull.boundingBox.offsetX = 12;
        bull.boundingBox.offsetY = 12;

        if(this.entity.direction == EDirection.Left)
            bull.setPosition(startPos.X - 16 - 8, startPos.Y - 40);

        else if(this.entity.direction == EDirection.Right)
            bull.setPosition(startPos.X - 16 + 8, startPos.Y - 40);



        this.entity.ingame.addBullet(bull, this.entity.isFriendly);

    }
}
