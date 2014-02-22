using SFML.Graphics;
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

            Sprite sp = new Sprite(Assets.fireballTexture);
            sp.TextureRect = new IntRect(0, 0, 32, 32);

            bull.setSprite(sp);
            bull.setBrain(new SimpleBullet(this.entity.direction));
            bull.setPhysics(new NoPhysics());

            bull.damage = 99;
            bull.boundingBox = new BoundingBox(this.entity.position.X, this.entity.position.Y, 32, 32);
            bull.setPosition(this.entity.position.X, this.entity.position.Y);

            //TODO: put in a possibilty to let enemies add their bullet list
        if(this.entity.isFriendly)
            this.entity.ingame.friendlyBullets.Add(bull);

        else
            this.entity.ingame.hostileBullets.Add(bull);

       
    }
}
