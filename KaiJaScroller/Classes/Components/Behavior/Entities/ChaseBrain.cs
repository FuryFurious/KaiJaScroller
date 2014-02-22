﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChaseBrain : ABehavior
{
    public ChaseBrain()
    {
        
    }

    public override void init()
    {
        ASkill skill1 = new Bomb();
        skill1.setAction(new RandomAction(0.5));
        skill1.setEntity(this.entity);

       skills.Add(skill1);
    }

    public override void update(GameTime gameTime)
    {
       //if (this.entity.ingame.player.position.X - this.entity.position.X < -5  && this.entity.canMoveLeft(-2))
      //      this.entity.moveHorz(-2);

       // else if (this.entity.ingame.player.position.X - this.entity.position.X > 5  && this.entity.canMoveRight(2))
        //    this.entity.moveHorz(2);

        updateSkills(gameTime);
    }

    public override void onKill()
    {

    }
}