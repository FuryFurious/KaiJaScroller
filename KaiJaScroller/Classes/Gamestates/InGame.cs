using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InGame : IGameState
{
    static String nextLevel;

    public List<BoundingBox> collisionRects = new List<BoundingBox>();
    public List<BoundingBox> harmfulArea = new List<BoundingBox>();

    public Entity player = new Entity();

    public List<Entity> friendlyBullets = new List<Entity>();
    public List<Entity> hostileBullets = new List<Entity>();

    public List<Entity> enemies = new List<Entity>();

    public List<DynamicText> text = new List<DynamicText>();

    List<Sprite> passiveSprites = new List<Sprite>();

    public List<Particle> particles = new List<Particle>();




    private RenderTarget[] targets;
    private RenderTexture finalTarget;

    private Teleporter teleport;

    Text fps;
    Sprite lifeframe;
    RectangleShape lifebar;
    float lifeWidth;

    View view;
    Sprite[, ,] sprites;

    EOverlayState currentOverlay = EOverlayState.None;
    EOverlayState prevOverlay;

    IOverlayState overlay;


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

        targets = new RenderTarget[2];

        finalTarget = new RenderTexture((uint)Settings.windowWidth, (uint)Settings.windowHeight);

        for (int i = 0; i < targets.Length; i++)
            targets[i] = new RenderTexture((uint)Settings.windowWidth, (uint)Settings.windowHeight);

        view = targets[0].GetView();
        view.Viewport = new FloatRect(view.Viewport.Left, view.Viewport.Top, 2f, 2f);
        targets[0].SetView(view);
    }



    public void init()
    {
        player = EntityLibrary.getEntity(EEntityType.Player);

        view = targets[0].GetView();
        view.Viewport = new FloatRect(view.Viewport.Left, view.Viewport.Top, Settings.windowWidth / Settings.viewportWidth, Settings.windowHeight / Settings.viewportHeight);

        targets[0].SetView(view);
        load();

        if (player.hitpoints[0] <= 0)
            player.hitpoints[0] = 1;

        loadLevel(nextLevel);

        foreach (Entity e in enemies)
            e.init(this);

        player.init(this);


        initViewport();
        //TODO: viewport position ist falsch, wenn spieler mit dem viewport nicht an 0,0


        updateLifebar();
        handleNewOverlayState();
    }

    private void initViewport()
    {
        view = targets[0].GetView();
        Vector2f playerCenter = player.boundingBox.Center;

        view.Center = (new Vector2f(playerCenter.X, view.Center.Y) + new Vector2f(Settings.viewportWidth / 2, 0));
        view.Center = (new Vector2f(view.Center.X, playerCenter.Y) + new Vector2f(0, Settings.viewportHeight / 2));

        if (view.Center.X - Settings.viewportWidth <= 0)
            view.Center = new Vector2f(Settings.viewportWidth, view.Center.Y);

        else if (view.Center.X + Settings.viewportWidth >= sprites.GetLength(1) * 32)
            view.Center = new Vector2f(sprites.GetLength(1) * 32, view.Center.Y);

        if (view.Center.Y - Settings.viewportHeight <= 0)
            view.Center = new Vector2f(view.Center.X, Settings.viewportHeight);

        else if (view.Center.Y + Settings.viewportHeight >= sprites.GetLength(0) * 32)
            view.Center = new Vector2f(view.Center.X, sprites.GetLength(0) * 32);



        targets[0].SetView(view);
    }

    private void updateViewport()
    {
        view = targets[0].GetView();

        Vector2f playerCenter = player.boundingBox.Center;

        if ((playerCenter.X > Settings.viewportWidth / 2 &&
            playerCenter.X + Settings.viewportWidth / 2 < sprites.GetLength(1) * 32))
            view.Center = (new Vector2f(playerCenter.X, view.Center.Y) + new Vector2f(Settings.viewportWidth / 2, 0));

        if ((playerCenter.Y > Settings.viewportHeight / 2 &&
            playerCenter.Y + Settings.viewportHeight / 2 < sprites.GetLength(0) * 32))
            view.Center = (new Vector2f(view.Center.X, playerCenter.Y) + new Vector2f(0, Settings.viewportHeight / 2));

        targets[0].SetView(view);
    }

    public EGameState update(GameTime gameTime)
    {

        currentOverlay = overlay.update(gameTime);

        if (currentOverlay != prevOverlay)
            handleNewOverlayState();

        if (GameStateManager.pad.isClicked(Help.RB))
            return EGameState.None;

        if (GameStateManager.pad.isClicked(Help.LB) || GameStateManager.input.isClicked(Keyboard.Key.Q))
            return EGameState.Restart;

        if (GameStateManager.input.isClicked(Keyboard.Key.F1) || GameStateManager.pad.isClicked(Help.Y))
            Settings.inDebug = !Settings.inDebug;

        if (!overlay.isPaused())
        {
            updateBattleText(gameTime);

            updateFriendlyBullets(gameTime);

            updateHostileBullets(gameTime);

            updateParticles(gameTime);

            this.player.update(gameTime);

            if (player.inviTime > 0)
            {
                if (player.inviTime > Settings.PLAYERINVITIME / 2 && player.canMoveRight(knockDirection.X, 0) && player.canMoveLeft(-knockDirection.X, 0) && !Settings.inDebug)
                    player.position += knockDirection;
                

                this.player.inviTime -= gameTime.ElapsedTime.TotalSeconds;
            }

            updateAoe(gameTime);


            if (Settings.inDebug)
                this.player.hitpoints[0] = this.player.hitpoints[1];
            

            updateEnemies(gameTime);





            if (player.boundingBox.intersects(teleport.bb))
            {
                nextLevel = teleport.targetMap;
                save();
                return EGameState.Restart;
            }


            updateViewport();
        }

        return returnGameState();
    }

    public void draw(GameTime gameTime, RenderWindow window)
    {


        if (Settings.inDebug)
        {
            fps.DisplayedString = "" + 1.0f / (float)gameTime.ElapsedTime.TotalSeconds;

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


        finalTarget.Clear(Color.Transparent);
        targets[0].Clear(Color.Transparent);
        targets[1].Clear(Color.Transparent);
        //targets[2].Clear(Color.Transparent);
        window.Clear(Color.Transparent);

        foreach (Sprite s in sprites)
            if (s != null)
                targets[0].Draw(s);

        foreach (Sprite s in passiveSprites)
            targets[0].Draw(s);

        foreach (Entity e in friendlyBullets)
            e.draw(gameTime, targets);

        foreach (Entity e in hostileBullets)
            e.draw(gameTime, targets);

        foreach (Entity e in enemies)
            e.draw(gameTime, targets);

        if (Settings.inDebug)
            foreach (BoundingBox bb in collisionRects)
                bb.draw(targets[0]);

        if (Settings.inDebug)
            teleport.draw(targets[0]);

        this.player.draw(gameTime, targets);

        foreach (Particle p in particles)
            p.draw(gameTime, targets[0]);

        foreach (DynamicText t in text)
            t.draw(gameTime, targets[0]);

        targets[1].Draw(lifeframe);
        targets[1].Draw(lifebar);

        if (Settings.inDebug)
            targets[1].Draw(fps);

        overlay.draw(gameTime, targets[1]);

        (targets[0] as RenderTexture).Display();
        (targets[1] as RenderTexture).Display();
        //(targets[2] as RenderTexture).Display();


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

    double aoeCooldown = 0;
    private void updateAoe(GameTime gameTime)
    {
        aoeCooldown -= gameTime.ElapsedTime.TotalSeconds;

        foreach (BoundingBox bb in harmfulArea)
        {
            if (aoeCooldown <= 0 && bb.intersects(player.boundingBox))
            {
                player.hitpoints[0] -= Settings.aoeDamage;
              //  player.inviTime = Settings.PLAYERINVITIME;

                DynamicText dT = new DynamicText(player.position, "" + Settings.aoeDamage, 3);
                text.Add(dT);

                aoeCooldown = Settings.aoeCooldown;

                updateLifebar();
            }

            
        }
    }

    private void updateParticles(GameTime gameTime)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].update(gameTime);

            if (particles[i].lifeTime <= 0)
            {
                particles.Remove(particles[i]);
                i--;
            }
        }
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
            friendlyBullets[i].update(gameTime);

            foreach (Entity e in enemies)
            {
                if (e.hitpoints[0] != int.MaxValue && e.inviTime <= 0 && friendlyBullets[i].boundingBox.intersects(e.boundingBox))
                {
                    friendlyBullets[i].hitpoints[0]--;

                    DynamicText dT = new DynamicText(e.position, "" + friendlyBullets[i].damage, 3);
                    text.Add(dT);

                    e.hitpoints[0] -= friendlyBullets[i].damage;
                    
                    
                    e.inviTime = Settings.ENEMYINVITIME;

                    particles.Add(EntityLibrary.getParticle(EParticleType.Blood, e.boundingBox.Center));
                    particles.Add(EntityLibrary.getParticle(EParticleType.Blood, e.boundingBox.Center));

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
           //     continue;
                i--;
            }
        }
    }

    private void updateEnemies(GameTime gameTime)
    {

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].update(gameTime);

            if (enemies[i].inviTime > 0)
                enemies[i].inviTime -= gameTime.ElapsedTime.TotalSeconds;

            if (player.inviTime <= 0 && enemies[i].damage > 0 && enemies[i].boundingBox.intersects(player.boundingBox))
            {

                DynamicText dT = new DynamicText(player.position, "" + enemies[i].damage, 3);
                text.Add(dT);

                player.inviTime = Settings.PLAYERINVITIME;
                player.hitpoints[0] -= enemies[i].damage;

                playerKnockback(enemies[i]);
                updateLifebar();
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

    Vector2f knockDirection;

    private void playerKnockback(Entity source)
    {
        player.jump(Settings.KNOCKBACKY);

        if (this.player.boundingBox.CenterX < source.boundingBox.CenterX)
            knockDirection = new Vector2f(-Settings.KNOCKBACKX, 0);

        else
            knockDirection = new Vector2f(Settings.KNOCKBACKX, 0);



        particles.Add(EntityLibrary.getParticle(EParticleType.Blood, player.boundingBox.Center));
        particles.Add(EntityLibrary.getParticle(EParticleType.Blood, player.boundingBox.Center));
    }

    private void updateHostileBullets(GameTime gameTime)
    {
        for (int i = 0; i < hostileBullets.Count; i++)
        {
            hostileBullets[i].update(gameTime);


            if (player.inviTime <= 0 && hostileBullets[i].boundingBox.intersects(player.boundingBox))
            {
                hostileBullets[i].hitpoints[0]--;

                DynamicText dT = new DynamicText(player.position, "" + hostileBullets[i].damage, 3);
                text.Add(dT);

                player.hitpoints[0] -= hostileBullets[i].damage;
                player.inviTime = Settings.PLAYERINVITIME;

                playerKnockback(hostileBullets[i]);

            
                updateLifebar();

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

    private void loadLevel(String path)
    {
        TiledMap.TiledMapInfo map = TiledMap.TiledMapInfo.getMap("Content/Level/" + path);

        int[, ,] ids = map.getTileIds();
        Texture texture = new Texture("Content/Level/" + map.getTileSetName() + ".png");
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
            else if (rec.type.Equals("Teleporter"))
            {
                teleport = new Teleporter(rec.x, rec.y, rec.width, rec.height, rec.name);
            }
            else if (rec.type.Equals("AoE"))
            {
                harmfulArea.Add(new BoundingBox(rec.x, rec.y, rec.width, rec.height));
            }

        }

        foreach (TiledMap.TiledPicture pic in map.pictures)
        {

            if (pic.type.Equals("EnemySpawnLeft"))
            {
                Entity ene = EntityLibrary.getEntity((EEntityType)pic.id);
                ene.setPosition(pic.x, pic.y);
                enemies.Add(ene);
            }

            else if (pic.type.Equals("EnemySpawnRight"))
            {
                Entity ene = EntityLibrary.getEntity((EEntityType)pic.id);
                ene.direction = EDirection.Right;
                ene.moveRight(0.0f);
                ene.setPosition(pic.x, pic.y);
                ene.gfxComp.mirror();
                enemies.Add(ene);
            }

            else if (pic.type.Equals("Picture"))
            {
                pic.id -= 1;

                int xPos = pic.id % ((int)texture.Size.X / map.getTileWidth());
                int yPos = pic.id / ((int)texture.Size.X / map.getTileHeight());

                xPos *= map.getTileWidth();
                yPos *= map.getTileHeight();

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

    private void updateLifebar()
    {

        if (((float)player.hitpoints[0] / (float)player.hitpoints[1]) > 0)
            lifebar.Size = new Vector2f((lifeWidth - 8.4f) * ((float)player.hitpoints[0] / (float)player.hitpoints[1]), 16);

        else
            lifebar.Size = new Vector2f(0, 0);
    }

    private void save()
    {
        Help.reader.open("Content/Other/save.txt");

        Help.reader.setValue("hp0", "" + player.hitpoints[0]);
        Help.reader.setValue("hp1", "" + player.hitpoints[1]);
        Help.reader.setValue("level", nextLevel);

        Help.reader.save();

        Help.reader.close();
    }

    private void load()
    {
        Help.reader.open("Content/Other/save.txt");

        int hp0 = int.Parse(Help.reader.getValue("hp0"));
        int hp1 = int.Parse(Help.reader.getValue("hp1"));

        player.hitpoints[0] = hp0;
        player.hitpoints[1] = hp1;

        nextLevel = Help.reader.getValue("level");

        Help.reader.close();
    }

    private void handleNewOverlayState()
    {
        switch (currentOverlay)
        {
            case EOverlayState.None:
                overlay = new NoneOverlay(player);
                break;

            case EOverlayState.GameOver:
                overlay = new GameOver();

                player.hitpoints[0] = player.hitpoints[1];
                save();

                break;

            case EOverlayState.Pause:
                overlay = new PauseOverlay();
                break;

            default:
                break;
        }

        overlay.init();
        prevOverlay = currentOverlay;
    }

    private EGameState returnGameState()
    {
        switch (currentOverlay)
        {
            case EOverlayState.Restart:
                return EGameState.Restart;

            case EOverlayState.MainMenu:
                return EGameState.MainMenu;

            default:
                return EGameState.InGame;
        }
    }

    public void addBullet(Entity e, bool friendly)
    {
        e.ingame = this;

        if (friendly)
            friendlyBullets.Add(e);
        else
            hostileBullets.Add(e);

    }


}