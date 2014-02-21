using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Entity
{
    //gfxComponent
    Sprite sprite;

    //update/think Component:
    ABrain brain;

    //physicComponent
    PhysicComponent physic;



    public FloatRect boundingBox;
    public Vector2f position;


    public Entity(Sprite s, ABrain b, PhysicComponent p)
    {
        this.sprite = s;
        this.brain = b;

        this.brain.setEntity(this);

        this.physic = p;
        this.physic.setEntity(this);
    }


    public void update(GameTime gameTime, InGame ingame)
    {
        brain.think(gameTime);
        physic.update(gameTime, ingame);
        //TODO: combatComponent update later


        this.sprite.Position = position;
        boundingBox.Left = position.X;
        boundingBox.Top = position.Y;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(sprite);

        Help.drawRectangle(this.boundingBox, target);
    }

    public void move(float x, float y)
    {
        this.position += new Vector2f(x, y);
    }

    public void setSprite(Sprite s)
    {
        this.sprite = s;
    }

    public void setBrain(ABrain b)
    {
        this.brain = b;
        this.brain.setEntity(this);
    }



}

