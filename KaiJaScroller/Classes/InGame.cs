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

    Entity player;

    public InGame()
    {
        this.player = new Entity(new Sprite(), null);

        List<Keyboard.Key> keys = new List<Keyboard.Key>();
        keys.Add(Keyboard.Key.W);
        keys.Add(Keyboard.Key.A);
        keys.Add(Keyboard.Key.S);
        keys.Add(Keyboard.Key.D);
        keys.Add(Keyboard.Key.Escape);
        keys.Add(Keyboard.Key.Space);

        input = new Input(keys);
    }

    public void init()
    {
    
    }

    public EGameState update(GameTime gameTime)
    {
        input.update();
        this.player.update(gameTime);

        return EGameState.InGame;
    }

    public void draw(GameTime gameTime, RenderWindow window)
    {
        this.player.draw(gameTime, window);
    }
}