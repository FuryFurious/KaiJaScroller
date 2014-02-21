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
    APhysicComponent physic;



    public BoundingBox boundingBox;
    public Vector2f position;

    InGame ingame;


    public Entity(Sprite s, ABrain b, APhysicComponent p)
    {
        this.sprite = s;
        this.brain = b;

        this.brain.setEntity(this);

        this.physic = p;
        this.physic.setEntity(this);
    }


    public void update(GameTime gameTime, InGame ingame)
    {
        this.ingame = ingame;
        physic.update(gameTime, ingame);
        brain.think(gameTime);

        //TODO: combatComponent update later


        //updates boundingBoxes and sprites:
        this.sprite.Position = position;
        boundingBox.X = position.X;
        boundingBox.Y = position.Y;
    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(sprite);

        boundingBox.draw(target);
    }

    public void moveHorz(float x)
    {
        this.position += new Vector2f(x, 0);
    }

    public void moveVert(float y)
    {
        this.position += new Vector2f(0, y);
    }

    public bool canMoveLeft(float dx)
    {
        float x = this.boundingBox.Left + dx;
        float y0 = this.boundingBox.Top + 3;
        float y1 = this.boundingBox.Bottom - 1;

        foreach (BoundingBox bb in ingame.collisionRects)
            if (bb.intersectsVertLine(x, y0, y1))
                return false;

        return true;
    }

    public bool canMoveRight(float dx)
    {
        float x = this.boundingBox.Right + dx;
        float y0 = this.boundingBox.Top + 3;
        float y1 = this.boundingBox.Bottom - 1;

        foreach (BoundingBox bb in ingame.collisionRects)
            if (bb.intersectsVertLine(x, y0, y1))
                return false;

        return true;
    }

    //TODO maybe remove +- 1 in verMovement:
    public bool canMoveDown(float dy)
    {
        float y = this.boundingBox.Bottom + dy;
        float x0 = this.boundingBox.Left + 1;
        float x1 = this.boundingBox.Right - 1;

        foreach (BoundingBox bb in ingame.collisionRects)
            if (bb.intersectsHorzLine(y, x0, x1))
                return false;

        return true;
    }

    //TODO maybe remove +- 1 in verMovement:
    public bool canMoveUp(float dy)
    {
        float y = this.boundingBox.Top + dy;
        float x0 = this.boundingBox.Left + 1;
        float x1 = this.boundingBox.Right - 1;

        foreach (BoundingBox bb in ingame.collisionRects)
            if (bb.intersectsHorzLine(y, x0, x1))
                return false;

        return true;
    }



    public void setPosition(float x, float y)
    {
        this.position = new Vector2f(x, y);
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

    public void setPhysics(APhysicComponent physics)
    {
        this.physic = physic;
        this.physic.setEntity(this);
    }

}

