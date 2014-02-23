using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TurretBrain : ABehavior, IActionListener
{
    float pX;
    float pY;
    float eX;
    float eY;
    bool canLeft;
    bool canRight;
    Fireball fireball;
    Bomb bomb;
    int count;

    public TurretBrain()
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
        canLeft = false;
        canRight = false;
        count = 0;

        bomb = new Bomb();
        bomb.setAction(this);
        bomb.setEntity(this.entity);
    }

    public override void update(GameTime gameTime)
    {
        pX = this.entity.ingame.player.boundingBox.CenterX;
        pY = this.entity.ingame.player.boundingBox.CenterY;
        eX = this.entity.boundingBox.CenterX;
        eY = this.entity.boundingBox.CenterY;
        count++;

        fireball.update(gameTime);



    }

    public override void onKill()
    {

    }

    public bool performed(GameTime gameTime, Entity source, String name)
    {
        if (name == fireball.name)
        {
            if (count > 100)
            {
                return true;
            }
            else
            {
                count = 0;
            }
        }

        else if (name == bomb.name)
        {
            //bomb.xSpeed = (float)Help.random.NextDouble() * 10.0f;


            return false;
        }

        return false;
    }
}