using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Game
{

    public static readonly Color CornflowerBlue = new Color(101, 156, 239);

    public RenderWindow     window;
    public GameTime         gameTime; 

    public static int wheelDelta;

    public Game(int width, int height, String title, Styles style)
    {
        window = new RenderWindow(new VideoMode((uint)width, (uint)height), title, style);

        window.SetMouseCursorVisible(true);

        window.Closed += closeHandler;
        window.MouseWheelMoved += mouseWheelHandler;
        
        window.SetVerticalSyncEnabled(true);
        window.SetFramerateLimit(60);

        gameTime = new GameTime();
    }

    private void mouseWheelHandler(object sender, MouseWheelEventArgs e)
    {
        wheelDelta = e.Delta;
    }

    private void closeHandler(object sender, EventArgs e)
    {
        window.Close();
    }

    public void run()
    {
        gameTime.Start();

        while (window.IsOpen())
        {
            window.DispatchEvents();

        
            gameTime.Update();

            update(gameTime);
            draw(gameTime, window);

            wheelDelta = 0;
            window.Display();
        }
    }
    
    public abstract void update(GameTime gameTime);

    public abstract void draw(GameTime gameTime, RenderWindow window);



}

