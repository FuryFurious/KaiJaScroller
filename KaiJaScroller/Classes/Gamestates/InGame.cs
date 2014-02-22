using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InGame : IGameState
{
    public List<BoundingBox> collisionRects = new List<BoundingBox>();

    public Entity player;

    public List<Entity> bullets = new List<Entity>();
    public List<Entity> enemies = new List<Entity>();

    public List<DynamicText> text = new List<DynamicText>();

    View view;
    Sprite[, ,] sprites;

    public InGame()
    {
        //   new Sprite(Assets.zombieTexture), 
                                 //   new PlayerBrain(), 
                                //    new SimplePhysic(new PlayerJump()));
        this.player = new Entity();
      
        this.player.setPhysics(new SimplePhysic(new PlayerJump()));
        this.player.boundingBox = new BoundingBox(0, 0, 16, 32);
        this.player.boundingBox.offsetX = 8;

        Sprite playerSprite = new Sprite(Assets.zombieTexture);
        playerSprite.TextureRect = new IntRect(0, 0, 32, 32);

        this.player.setSprite(playerSprite);

        PlayerBrain brain = new PlayerBrain();
        this.player.setBrain(brain);

        brain.init();





        Entity enemy1;
        enemy1 = new Entity();//   new Sprite(Assets.impTexture), 
                                //    new NoBrain(), 
                                  //  new SimplePhysic(new RandomJump()));

        Sprite s = new Sprite(Assets.impTexture);
        s.TextureRect = new IntRect(0, 0, 32, 32);
        enemy1.setSprite(s);
        enemy1.setBrain(new ChaseBrain());
        enemy1.setPhysics(new SimplePhysic(new NoAction()));

        enemy1.damage = 42;
        enemy1.hitpoints = int.MaxValue;
        enemy1.boundingBox = new BoundingBox(0, 0, 32, 32);
        enemy1.position = new Vector2f(240, 50);

        enemies.Add(enemy1);


        fillSprites("Content/testLevel.tmx");

        
    }

    public void init()
    {
    
    }

    public EGameState update(GameTime gameTime)
    {

        if (GameStateManager.pad.isClicked(Help.LB) || GameStateManager.input.isClicked(Keyboard.Key.Escape))
            return EGameState.Restart;

        if (GameStateManager.input.isClicked(Keyboard.Key.F1))
            Settings.drawBoundings = !Settings.drawBoundings;


        for (int i = 0; i < text.Count; i++)
        {
            text[i].update(gameTime);

            if (text[i].lifeTime <= 0)
            {
                text.Remove(text[i]);
                i--;
            }
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].update(gameTime, this);

            foreach (Entity e in enemies)
            {
                if (e.inviTime <= 0 && bullets[i].boundingBox.intersects(e.boundingBox))
                {
                    bullets[i].hitpoints--;

                    DynamicText dT = new DynamicText(e.position, "" + bullets[i].damage, 3);
                    text.Add(dT);

                    e.hitpoints -= bullets[i].damage;
                    e.inviTime = Settings.INVITIME;

                    if (bullets[i].hitpoints <= 0)
                    {
                        bullets[i].exists = false;
                        break;
                    }

                }
            }

            if (!bullets[i].exists)
            {
                bullets[i].onKill();
                this.bullets.Remove(bullets[i]); 
                i--;
            }
        }


        this.player.update(gameTime, this);

        if(player.inviTime > 0)
            this.player.inviTime -= gameTime.ElapsedTime.TotalSeconds;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].update(gameTime, this);

            if (enemies[i].inviTime > 0)
                enemies[i].inviTime -= gameTime.ElapsedTime.TotalSeconds;

            if (player.inviTime <= 0 && enemies[i].boundingBox.intersects(player.boundingBox))
            {
                DynamicText dT = new DynamicText(player.position, "" + enemies[i].damage, 3);
                text.Add(dT);

                player.inviTime = Settings.INVITIME;
                player.hitpoints -= enemies[i].damage;
            }

            if (enemies[i].hitpoints <= 0)
                enemies[i].exists = false;

            if (!enemies[i].exists)
            {
                enemies[i].onKill();
                this.enemies.Remove(enemies[i]);
                i--;
            }
        }

        return EGameState.InGame;
    }

    public void draw(GameTime gameTime, RenderWindow window)
    {
        if (GameStateManager.input.leftPressed())
        {
            view = window.GetView();
            view.Center -= GameStateManager.input.getDeltaMousePos();

            window.SetView(view);
        }

        if (GameStateManager.input.mouseWheelDown())
        {
            view = window.GetView();
            view.Zoom(1.1f);
            window.SetView(view);
        }

        else if (GameStateManager.input.mouseWheelUp())
        {
            view = window.GetView();
            view.Zoom(0.9f);
            window.SetView(view);
        }


        window.Clear(Game.CornflowerBlue);

        foreach (Sprite s in sprites)
            if(s != null)
                window.Draw(s);

        foreach (Entity e in bullets)
            e.draw(gameTime, window);

        foreach (Entity e in enemies)
            e.draw(gameTime, window);

        if (Settings.drawBoundings)
            foreach (BoundingBox bb in collisionRects)
                bb.draw(window);

        this.player.draw(gameTime, window);

        foreach (DynamicText t in text)
            t.draw(gameTime, window);

    }

    //TODO: remove to better place:
    private void fillSprites(String path)
    {
        TiledMap.TiledMapInfo map = TiledMap.TiledMapInfo.getMap(path);

        int[, ,] ids = map.getTileIds();
        Texture texture = new Texture("Content/" + map.getTileSetName() + ".png");
        sprites = new Sprite[ids.GetLength(0), ids.GetLength(1), ids.GetLength(2)];


        foreach (TiledMap.TiledRectangle rec in map.rectangles)
        {

            if (rec.type.Equals("Collision"))
            {
                collisionRects.Add(new BoundingBox(rec.x, rec.y, rec.width, rec.height));
            }
            else if (rec.type.Equals("PlayerSpawn"))
            {
                player.setPosition(rec.x, rec.y);
            }

        }


        for (int z = 0; z < ids.GetLength(2); z++)
        {
            for (int x = 0; x < ids.GetLength(0); x++)
            {
                for (int y = 0; y < ids.GetLength(1); y++)
                {
                    int id = ids[x, y, z] - 1;

                    int xPos = id % ((int)texture.Size.X / map.getTileWidth());
                    int yPos = id / ((int)texture.Size.X / map.getTileHeight());

                    if (id != -1)
                    {
                        sprites[x, y, z] = new Sprite(texture);
                        sprites[x, y, z].TextureRect = new IntRect(xPos * map.getTileWidth(), yPos * map.getTileHeight(), map.getTileWidth(), map.getTileHeight());
                        sprites[x, y, z].Position = new Vector2f(y * map.getTileHeight(), x * map.getTileWidth());
                    }

                }
            }
        }
    }
}