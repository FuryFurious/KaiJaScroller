using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InGame : IGameState
{
    static String nextLevel = "testLevel.tmx";

    public List<BoundingBox> collisionRects = new List<BoundingBox>();

    public Entity player;

    public List<Entity> friendlyBullets = new List<Entity>();
    public List<Entity> hostileBullets = new List<Entity>();
    public List<Entity> enemies             = new List<Entity>();

    public List<DynamicText> text           = new List<DynamicText>();

    List<Sprite> passiveSprites             = new List<Sprite>();

    private RenderTarget[] targets;
    private RenderTexture finalTarget;

    private Teleporter teleport;

    Text fps;
    Sprite lifeframe;
    RectangleShape lifebar;
    float lifeWidth;

    View view;
    Sprite[, ,] sprites;

    public InGame()
    {
        fps = new Text("", Assets.font1);
        lifeframe = new Sprite(Assets.lifebar);
        lifeframe.Position = new Vector2f(20, 15);
        lifebar = new RectangleShape();

        lifebar.Position = new Vector2f(24, 23);
        lifebar.FillColor = Color.Red;
        lifebar.Size = new Vector2f(1, 16);

        lifeWidth = (float)Assets.lifebar.Size.X;

        targets = new RenderTarget[3];

        finalTarget = new RenderTexture((uint)Settings.windowWidth, (uint)Settings.windowHeight);

        for(int i = 0; i < targets.Length; i++)
            targets[i] = new RenderTexture((uint)Settings.windowWidth, (uint)Settings.windowHeight);


        this.player = new Entity();
      
        this.player.setPhysics(new SimplePhysic(new PlayerJump()));
        this.player.boundingBox = new BoundingBox(0, 0, 16, 32);
        this.player.boundingBox.offsetX = 8;

        Sprite playerSprite = new Sprite(Assets.zombieTexture);
        playerSprite.TextureRect = new IntRect(0, 0, 32, 32);

        this.player.setSprite(playerSprite);
        this.player.hitpoints[1] = 300;
        this.player.hitpoints[0] = this.player.hitpoints[1];

        PlayerBrain brain = new PlayerBrain();
        this.player.setBrain(brain);
        
      

        view = targets[0].GetView();
        view.Viewport = new FloatRect(view.Viewport.Left, view.Viewport.Top, Settings.windowWidth/Settings.viewportWidth, Settings.windowHeight/Settings.viewportHight);
        targets[0].SetView(view);

        loadLevel(nextLevel);
    }

    public void init()
    {
        foreach (Entity e in enemies)
            e.init();

        player.init();
    }

    public EGameState update(GameTime gameTime)
    {

        if (GameStateManager.pad.isClicked(Help.LB) || GameStateManager.input.isClicked(Keyboard.Key.Escape))
            return EGameState.Restart;

        if (GameStateManager.pad.isClicked(Help.RB) || GameStateManager.input.isClicked(Keyboard.Key.Escape))
            return EGameState.None;

        if (GameStateManager.input.isClicked(Keyboard.Key.F1))
            Settings.drawBoundings = !Settings.drawBoundings;


        updateBattleText(gameTime);

        updateFriendlyBullets(gameTime);

        updateHostileBullets(gameTime);

        this.player.update(gameTime, this);

        if(player.inviTime > 0)
            this.player.inviTime -= gameTime.ElapsedTime.TotalSeconds;

        updateEnemies(gameTime);

        if (((float)player.hitpoints[0] / (float)player.hitpoints[1]) > 0)
            lifebar.Size = new Vector2f((lifeWidth - 8.4f) * ((float)player.hitpoints[0] / (float)player.hitpoints[1]), 16);
        else lifebar.Size = new Vector2f(0, 0);        if (player.boundingBox.intersects(teleport.bb))
        {
            nextLevel = teleport.targetMap;
            return EGameState.Restart;
        }
        return EGameState.InGame;
    }

    public void draw(GameTime gameTime, RenderWindow window)
    {
        if (Settings.drawBoundings)
        {
            if (GameStateManager.input.leftPressed())
            {
                view = targets[0].GetView();
                view.Center -= GameStateManager.input.getDeltaMousePos();

                targets[0].SetView(view);
            }

            if (GameStateManager.input.mouseWheelDown())
            {
                view = targets[0].GetView();
                view.Zoom(1.1f);
                targets[0].SetView(view);
            }

            else if (GameStateManager.input.mouseWheelUp())
            {
                view = targets[0].GetView();
                view.Zoom(0.9f);
                targets[0].SetView(view);
            }
        }


        
        view = targets[0].GetView();
        view.Center = (player.boundingBox.Center + new Vector2f(200, 150));
        targets[0].SetView(view);


        finalTarget.Clear(Color.Transparent);
        targets[0].Clear(Color.Transparent);
        targets[1].Clear(Color.Transparent);
        targets[2].Clear(Color.Transparent);
        window.Clear(Color.Transparent);

        foreach (Sprite s in sprites)
            if(s != null)
                targets[0].Draw(s);

        foreach (Sprite s in passiveSprites)
            targets[0].Draw(s);

        foreach (Entity e in friendlyBullets)
            e.draw(gameTime, targets);

        foreach (Entity e in hostileBullets)
            e.draw(gameTime, targets);

        foreach (Entity e in enemies)
            e.draw(gameTime, targets);

        if (Settings.drawBoundings)
            foreach (BoundingBox bb in collisionRects)
                bb.draw(targets[0]);

        if(Settings.drawBoundings)
            teleport.draw(targets[0]);

        this.player.draw(gameTime, targets);

        foreach (DynamicText t in text)
            t.draw(gameTime, targets[0]);




        fps.DisplayedString = "" + 1.0f / (float)gameTime.ElapsedTime.TotalSeconds;
        if (Settings.drawBoundings)
            targets[1].Draw(fps);

        

        targets[1].Draw(lifeframe);
        targets[1].Draw(lifebar);


        (targets[0] as RenderTexture).Display();
        (targets[1] as RenderTexture).Display();
        (targets[2] as RenderTexture).Display();


        finalTarget.Draw(new Sprite((targets[0] as RenderTexture).Texture));
        finalTarget.Draw(new Sprite((targets[1] as RenderTexture).Texture));

        if (!player.exists)
        {
            (targets[2] as RenderTexture).Display();
            finalTarget.Draw(new Sprite((targets[2] as RenderTexture).Texture));
        }

        finalTarget.Display();

        window.Draw(new Sprite(finalTarget.Texture));

    }

    private void updateBattleText(GameTime gameTime)
    {
        for (int i = 0; i < text.Count; i++)
        {
            text[i].update(gameTime);

            if (text[i].lifeTime <= 0)
            {
                text.Remove(text[i]);
                i--;
            }
        }
    }

    private void updateFriendlyBullets(GameTime gameTime)
    {
        for (int i = 0; i < friendlyBullets.Count; i++)
        {
            friendlyBullets[i].update(gameTime, this);

            foreach (Entity e in enemies)
            {
                if (e.inviTime <= 0 && friendlyBullets[i].boundingBox.intersects(e.boundingBox))
                {
                    friendlyBullets[i].hitpoints[0]--;

                    DynamicText dT = new DynamicText(e.position, "" + friendlyBullets[i].damage, 3);
                    text.Add(dT);

                    e.hitpoints[0] -= friendlyBullets[i].damage;
                    e.inviTime = Settings.INVITIME;

                    if (friendlyBullets[i].hitpoints[0] <= 0)
                    {
                        friendlyBullets[i].exists = false;
                        break;
                    }

                }
            }

            if (!friendlyBullets[i].exists)
            {
                friendlyBullets[i].onKill();
                this.friendlyBullets.Remove(friendlyBullets[i]);
                i--;
            }
        }
    }

    private void updateEnemies(GameTime gameTime)
    {
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
                player.hitpoints[0] -= enemies[i].damage;
            }

            if (enemies[i].hitpoints[0] <= 0)
                enemies[i].exists = false;

            if (!enemies[i].exists)
            {
                enemies[i].onKill();
                this.enemies.Remove(enemies[i]);
                i--;
            }
        }
    }

    private void updateHostileBullets(GameTime gameTime)
    {
        for (int i = 0; i < hostileBullets.Count; i++)
        {
            hostileBullets[i].update(gameTime, this);


            if (player.inviTime <= 0 && hostileBullets[i].boundingBox.intersects(player.boundingBox))
            {
                hostileBullets[i].hitpoints[0]--;

                DynamicText dT = new DynamicText(player.position, "" + hostileBullets[i].damage, 3);
                text.Add(dT);

                player.hitpoints[0] -= hostileBullets[i].damage;
                player.inviTime = Settings.INVITIME;

                if (hostileBullets[i].hitpoints[0] <= 0)
                {
                    hostileBullets[i].exists = false;
                    break;
                }

            }
            

            if (!hostileBullets[i].exists)
            {
                hostileBullets[i].onKill();
                this.hostileBullets.Remove(hostileBullets[i]);
                i--;
            }
        }
    }

    //TODO: remove to better place:
    private void loadLevel(String path)
    {
        TiledMap.TiledMapInfo map = TiledMap.TiledMapInfo.getMap("Content/"+path);

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
            if (rec.type.Equals("Teleporter"))
            {
                teleport = new Teleporter(rec.x, rec.y, rec.width, rec.height, rec.name);
            }

        }

        foreach (TiledMap.TiledPicture pic in map.pictures)
        {

            if (pic.type.Equals("EnemySpawn"))
            {
                Entity ene = EntityLibrary.getEntity((EEntityType)pic.id);
                ene.setPosition(pic.x, pic.y);
                enemies.Add(ene);
            }
            else if (pic.type.Equals("Picture"))
            {
                pic.id -= 1;

                int xPos = pic.id % ((int)texture.Size.X / map.getTileWidth());
                int yPos = pic.id / ((int)texture.Size.X / map.getTileHeight());

                xPos *= map.getTileWidth();
                yPos *= map.getTileHeight();

                Console.WriteLine(pic.id);
                Console.WriteLine(pic.x + ", " + pic.y);

                Sprite s = new Sprite(texture);

                s.TextureRect = new IntRect(xPos, yPos, map.getTileWidth(), map.getTileHeight());
                s.Position = new Vector2f(pic.x, pic.y - map.getTileHeight());

                passiveSprites.Add(s);
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