using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerBrain : ABehavior, IActionListener
{
    float speed = 3;

    public List<ASkill> skills = new List<ASkill>();
    Animation animation;

    public PlayerBrain()
    {
        animation = new Animation();
        animation.animationTime = 0.2;
        animation.numPics = 4;

       

    }

    public override void update(GameTime gameTime)
    {
        if(this.entity.inviTime < Settings.PLAYERINVITIME / 2)
            updateMovement();

        animation.update(gameTime);

        

        updateSkills(gameTime);
    }

    private void updateMovement()
    {
        if (GameStateManager.input.isPressed(Keyboard.Key.A) && this.entity.canMoveLeft(speed, 0))
            this.entity.moveLeft(speed);

        if (GameStateManager.input.isClicked(Keyboard.Key.Space) || GameStateManager.pad.isClicked(Help.A))
            this.entity.jump(5);

        else if (GameStateManager.input.isPressed(Keyboard.Key.D) && this.entity.canMoveRight(speed, 0))
        {
            this.entity.moveRight(speed);
        }

        float leftX = GameStateManager.pad.getLeftX();
     

        if (leftX > 0)
            leftX = ((Help.Clamp(leftX, 5, 95) - 5.0f) / 90.0f);

        else if (leftX < 0)
            leftX = ((Help.Clamp(leftX, -95, -5) + 5.0f) / 90.0f);


        if (Math.Abs(leftX) > 0.2f)
        {
            float xHelp = leftX * speed;

            if (xHelp > 0 && this.entity.canMoveRight(xHelp, 0))
                this.entity.moveRight(xHelp);

            else if (xHelp < 0 && this.entity.canMoveLeft(-xHelp, 0))
                this.entity.moveLeft(-xHelp);
        }
    }

    public override void onKill()
    {

    }


    public override void init()
    {
        ASkill skill1 = new SmallSword();
        skill1.setAction(this);
        skill1.setEntity(this.entity);

       (skill1 as SmallSword).reach = 17;
      //  (skill1 as SmallSword).lifeTime = 0.5;
       // (skill1 as SmallSword).count = 10;
      //  (skill1 as SmallSword).xSpeed = 100;

        ASkill skill2 = new Bomb();
        skill2.setAction(this);
        skill2.setEntity(this.entity);

        skills.Add(skill1);
        skills.Add(skill2);

        animation.entity = this.entity;

    
    }


    public void updateSkills(GameTime gameTime)
    {

        foreach (ASkill skill in skills)
            skill.update(gameTime);
    }




    public bool performed(GameTime gameTime, Entity source, string name)
    {
        bool returnBool = false;

        if(name == skills[0].name)
            returnBool = GameStateManager.input.isClicked(Keyboard.Key.E) || GameStateManager.pad.isClicked(Help.X);

        else if(name == skills[1].name)
            returnBool = GameStateManager.input.isClicked(Keyboard.Key.R) || GameStateManager.pad.isClicked(Help.B);

        if (returnBool)
            animation.start();


        return returnBool;
    }
}