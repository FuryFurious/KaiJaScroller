using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBrain : ABehavior , IActionListener
{

    Fireball fireball;
    Bomb bomb;

    public ChaseBrain()
    {
        

    }

    public override void init()
    {
        fireball = new Fireball();
        fireball.setAction(this);
        fireball.setEntity(this.entity);
        //ASkill skill1 = new Fireball();
        //skill1.setAction(new RandomAction(0.5));
        //skill1.setEntity(this.entity);

        //skills.Add(skill1);
        bomb = new Bomb();
        bomb.setAction(this);
        bomb.setEntity(this.entity);
    }

    public override void update(GameTime gameTime)
    {
        if (this.entity.ingame.player.position.X - this.entity.position.X < -5 && this.entity.ingame.player.position.X - this.entity.position.X > -150 && this.entity.canMoveLeft(2, 0))
            this.entity.moveLeft(2);

        else if (this.entity.ingame.player.position.X - this.entity.position.X > 5 && this.entity.ingame.player.position.X - this.entity.position.X < 150 && this.entity.canMoveRight(2, 0))
            this.entity.moveRight(2);

   //     fireball.update(gameTime);

  //      bomb.update(gameTime);

        //updateSkills(gameTime);
    }

    public override void onKill()
    {

    }

    public bool performed(GameTime gameTime, Entity source, String name)
    {
        if (name == fireball.name)
        {
            return true;
        }

        else if (name == bomb.name)
        {
            //bomb.xSpeed = (float)Help.random.NextDouble() * 10.0f;

            return true;
        }

        return false;
    }
}