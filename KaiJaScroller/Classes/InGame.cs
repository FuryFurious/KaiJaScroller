using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InGame : IGameState
{
    public static Input input;
    public static Gamepad pad = new Gamepad();

    public BoundingBox[] collisionRects;

    public Entity player;

    public List<Entity> bullets = new List<Entity>();
    public List<Entity> enemies = new List<Entity>();


    View view;
    Sprite[, ,] sprites;

    public InGame()
    {
        this.player = new Entity(   new Sprite(Assets.golemTexture), 
                                    new PlayerBrain(), 
                                    new SimplePhysic(new PlayerJump()));

        this.player.boundingBox = new BoundingBox(0, 0, 32, 32);


        Entity enemy1;
        enemy1 = new Entity(   new Sprite(Assets.impTexture), 
                                    new NoBrain(), 
                                    new SimplePhysic(new RandomJump()));

        enemy1.damage = 1;
        enemy1.hitpoints = int.MaxValue;
        enemy1.boundingBox = new BoundingBox(0, 0, 32, 32);
        enemy1.position = new Vector2f(240, 50);

        enemies.Add(enemy1);

        List<Keyboard.Key> keys = new List<Keyboard.Key>();
        keys.Add(Keyboard.Key.W);
        keys.Add(Keyboard.Key.A);
        keys.Add(Keyboard.Key.S);
        keys.Add(Keyboard.Key.D);
        keys.Add(Keyboard.Key.E);
        keys.Add(Keyboard.Key.Q);
        keys.Add(Keyboard.Key.F1);
        keys.Add(Keyboard.Key.Escape);
        keys.Add(Keyboard.Key.Space);

        input = new Input(keys);

        fillSprites("Content/testLevel.tmx");

        
    }

    public void init()
    {
    
    }

    public EGameState update(GameTime gameTime)
    {
        input.update();
        pad.update();

        if (input.isClicked(Keyboard.Key.F1))
            Settings.drawBoundings = !Settings.drawBoundings;


      //  for (int i = 0; i < combatText.Count; i++)
        {

        }

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].update(gameTime, this);

            foreach (Entity e in enemies)
            {
                if (e.inviTime <= 0 && bullets[i].boundingBox.intersects(e.boundingBox))
                {
                    bullets[i].hitpoints--;

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

            if(enemies[i].inviTime > 0)
                enemies[i].inviTime -= gameTime.ElapsedTime.TotalSeconds;

            if (player.inviTime <= 0 && enemies[i].boundingBox.intersects(player.boundingBox))
            {
                player.inviTime = Settings.INVITIME;
                player.hitpoints -= enemies[i].damage;
            }

            if (enemies[i].hitpoints <= 0)
                enemies[i].exists = false;

            if (!enemies[i].exists)
                this.enemies.Remove(enemies[i]);
        }

        return EGameState.InGame;
    }

    public void draw(GameTime gameTime, RenderWindow window)
    {
        if (input.leftPressed())
        {
            view = window.GetView();
            view.Center -= input.getDeltaMousePos();
            window.SetView(view);
        }

        if (input.mouseWheelDown())
        {
            view = window.GetView();
            view.Zoom(1.1f);
            window.SetView(view);
        }
        else if (input.mouseWheelUp())
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


    }

    //TODO: remove to better place:
    private void fillSprites(String path)
    {
        TiledMap.TiledMapInfo map = TiledMap.TiledMapInfo.getMap(path);

        int[, ,] ids = map.getTileIds();
        Texture texture = new Texture("Content/" + map.getTileSetName() + ".png");
        sprites = new Sprite[ids.GetLength(0), ids.GetLength(1), ids.GetLength(2)];

        collisionRects = new BoundingBox[map.rectangles.Count];

        for (int i = 0; i < collisionRects.Length; i++)
        {
            TiledMap.TiledRectangle r = map.rectangles[i];
            collisionRects[i] = new BoundingBox(r.x, r.y, r.width, r.height);
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