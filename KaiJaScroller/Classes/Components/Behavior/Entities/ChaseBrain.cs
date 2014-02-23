using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBrain : ABehavior
{

    float pX;
    float eX;
    float pY;
    float eY;

    public ChaseBrain()
    {
        
    }

    public override void init()
    {
        //ASkill skill1 = new Fireball();
        //skill1.setAction(new RandomAction(0.5));
        //skill1.setEntity(this.entity);

        //skills.Add(skill1);
         pX = this.entity.ingame.player.boundingBox.CenterX;
    eX = this.entity.boundingBox.CenterX;
    pY = this.entity.ingame.player.boundingBox.CenterY;
    eY = this.entity.boundingBox.CenterY;
        
    }

    public override void update(GameTime gameTime)
    {
        if (pX - eX < -5 && pX - eX > -150 && this.entity.canMoveLeft(2, 0))
            this.entity.moveLeft(2);

        else if (this.entity.ingame.player.position.X - this.entity.position.X > 5 && this.entity.ingame.player.position.X - this.entity.position.X < 150 && this.entity.canMoveRight(2, 0))
            this.entity.moveRight(2);

        updateSkills(gameTime);
    }

    public override void onKill()
    {

    }
}