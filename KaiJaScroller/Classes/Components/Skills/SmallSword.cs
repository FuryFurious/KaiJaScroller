using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SmallSword : ASkill
{

    public float xSpeed = 5.0f;
    

    public SmallSword()
    {
        this.name = "SmallSword";

        this.maxCooldown = 0.5;
        this.curCooldown = 0.5;
        
    }

    public override void onUse(GameTime gameTime)
    {
        Entity bull = new Entity();

        Sprite sp = new Sprite(Assets.smallSwordTexture);
        sp.TextureRect = new IntRect(0, 0, 32, 32);

        AGfxComp comp = new ProjectileGfx();
        comp.setSprite(sp);


        SimpleSword s = new SimpleSword(this.entity.direction);
        s.xSpeed = xSpeed;
       
        bull.setGfxComp(comp);
        bull.setBrain(s);
        bull.setPhysics(new NoPhysics());
        
        Vector2f startPos = this.entity.boundingBox.Center;

        bull.damage = 99;
        bull.boundingBox = new BoundingBox(startPos.X, startPos.Y, 16, 6);
        bull.boundingBox.offsetX = 7;
        bull.boundingBox.offsetY = 13;
        s.e = this.entity;

        if (this.entity.direction == EDirection.Left)
        {
            bull.moveRight(0);   
            bull.setPosition(startPos.X - 16 - 8, startPos.Y - 12);
        }
        else if (this.entity.direction == EDirection.Right)
        {
            
            bull.setPosition(startPos.X - 16 + 8, startPos.Y   -12);
        }
        comp.init();

      //  bull.init(this.entity.ingame);
        

        this.entity.ingame.addBullet(bull, this.entity.isFriendly);

        
    }
}
