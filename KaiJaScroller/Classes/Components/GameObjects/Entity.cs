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
    ABehavior behavior;

    //physicComponent
    APhysicComponent physic;


    public bool isFriendly = true;

    public bool exists = true;

    public BoundingBox boundingBox;
    public Vector2f position;

    public InGame ingame;

    public int damage = 0;
    public int[] hitpoints = new int[2];

    public double inviTime = Settings.INVITIME;
    public EDirection direction = EDirection.Left;

    public Entity()
    {
        hitpoints[0] = 1;
        hitpoints[1] = 1;



    }

    public void init()
    {
        
        behavior.init();
        physic.init();
    }


    public void update(GameTime gameTime, InGame ingame)
    {
        this.ingame = ingame;
        physic.update(gameTime);
        behavior.update(gameTime);

        this.sprite.Position = position;
        boundingBox.X = position.X + boundingBox.offsetX;
        boundingBox.Y = position.Y + boundingBox.offsetY;
    }

    public void draw(GameTime gameTime, RenderTarget[] target)
    {
        target[0].Draw(sprite);

        if(Settings.drawBoundings)
            boundingBox.draw(target[0]);
    }

    public void moveHorz(float x)
    {
        if (x < 0)
        {
            this.direction = EDirection.Left;
            this.sprite.TextureRect = new IntRect(0, 0, this.sprite.TextureRect.Width, this.sprite.TextureRect.Height);
        }
        else if (x > 0)
        {
            this.direction = EDirection.Right;
            this.sprite.TextureRect = new IntRect(this.sprite.TextureRect.Width, 0, this.sprite.TextureRect.Width, this.sprite.TextureRect.Height);
        }
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

    //TODO maybe remove +- 1 in verMovement, written for offsetting stuff, maybe its not good!
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
        this.sprite.Position = this.position;
    }


  

    public void setSprite(Sprite s)
    {
        this.sprite = s;
    }

    public void setBrain(ABehavior b)
    {
        this.behavior = b;
        this.behavior.setEntity(this);
    }

    public void setPhysics(APhysicComponent physic)
    {
        this.physic = physic;
        this.physic.setEntity(this);
    }

    public void onKill()
    {
        behavior.onKill();
    }

}

