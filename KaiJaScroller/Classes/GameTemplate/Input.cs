using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Input
{
    List<Keyboard.Key> usedKeys;

    bool[] oldKeys;
    bool[] currentKeys;

    bool[] oldMouse;
    bool[] currentMouse;

    Vector2i oldMousePos;
    Vector2i currentMousePos;


    public Input(List<Keyboard.Key> keys)
    {
        this.usedKeys = keys;

        oldKeys = new bool[(int)Keyboard.Key.KeyCount];
        currentKeys = new bool[(int)Keyboard.Key.KeyCount];

        oldMouse = new bool[(int)Mouse.Button.ButtonCount];
        currentMouse = new bool[(int)Mouse.Button.ButtonCount];
    }


    public void update()
    {
        oldMousePos = currentMousePos;
        currentMousePos = Mouse.GetPosition();



        foreach(Keyboard.Key key in usedKeys)
            oldKeys[(int)key] = currentKeys[(int)key];

        //update every Key that is needed:
        foreach (Keyboard.Key key in usedKeys)
            if (Keyboard.IsKeyPressed(key))
                currentKeys[(int)key] = true;
            else
                currentKeys[(int)key] = false;

        for (int i = 0; i < oldMouse.Length; i++)
            oldMouse[i] = currentMouse[i];

        //update every mouseKey:
        for (int i = 0; i < oldMouse.Length; i++)
            if (Mouse.IsButtonPressed((Mouse.Button)i))
                currentMouse[i] = true;
            else
                currentMouse[i] = false;   


    }

    public Vector2f getDeltaMousePos()
    {
        return new Vector2f(currentMousePos.X - oldMousePos.X, currentMousePos.Y - oldMousePos.Y);
    }

    public bool isClicked(Keyboard.Key key)
    {
        return currentKeys[(int)key] && !oldKeys[(int)key];
    }

    public bool isPressed(Keyboard.Key key)
    {
        return Keyboard.IsKeyPressed(key);
    }

    public bool isReleased(Keyboard.Key key)
    {
        return oldKeys[(int)key] && !Keyboard.IsKeyPressed(key);
    }



    public bool leftClicked()
    {
        return !oldMouse[(int)Mouse.Button.Left] && currentMouse[(int)Mouse.Button.Left];
    }

    public bool leftPressed()
    {
        return currentMouse[(int)Mouse.Button.Left];
    }

    public bool leftReleased()
    {
        return oldMouse[(int)Mouse.Button.Left] && !currentMouse[(int)Mouse.Button.Left];
    }




    public bool rightClicked()
    {
        return !oldMouse[(int)Mouse.Button.Right] && currentMouse[(int)Mouse.Button.Right];
    }

    public bool rightPressed()
    {
        return currentMouse[(int)Mouse.Button.Right];
    }

    public bool rightReleased()
    {
        return oldMouse[(int)Mouse.Button.Right] && !currentMouse[(int)Mouse.Button.Right];
    }


    public bool midClicked()
    {
        return !oldMouse[(int)Mouse.Button.Middle] && currentMouse[(int)Mouse.Button.Middle];
    }

    public bool midPressed()
    {
        return currentMouse[(int)Mouse.Button.Middle];
    }

    public bool midReleased()
    {
        return oldMouse[(int)Mouse.Button.Middle] && !currentMouse[(int)Mouse.Button.Middle];
    }


    public bool mouseWheelUp()
    {
        return Game.wheelDelta == 1;
    }

    public bool mouseWheelDown()
    {
        return Game.wheelDelta == -1;
    }

    public static void getNumKeys(ref List<Keyboard.Key> keys)
    {
        keys.Add(Keyboard.Key.Num0);
        keys.Add(Keyboard.Key.Num1);
        keys.Add(Keyboard.Key.Num2);
        keys.Add(Keyboard.Key.Num3);
        keys.Add(Keyboard.Key.Num4);
        keys.Add(Keyboard.Key.Num5);
        keys.Add(Keyboard.Key.Num6);
        keys.Add(Keyboard.Key.Num7);
        keys.Add(Keyboard.Key.Num8);
        keys.Add(Keyboard.Key.Num9);
    }

    public static void getCharKeys(ref List<Keyboard.Key> keys)
    {

        keys.Add(Keyboard.Key.A);
        keys.Add(Keyboard.Key.B);
        keys.Add(Keyboard.Key.C);

        keys.Add(Keyboard.Key.D);
        keys.Add(Keyboard.Key.E);
        keys.Add(Keyboard.Key.F);

        keys.Add(Keyboard.Key.G);
        keys.Add(Keyboard.Key.H);
        keys.Add(Keyboard.Key.I);

        keys.Add(Keyboard.Key.J);
        keys.Add(Keyboard.Key.K);
        keys.Add(Keyboard.Key.L);

        keys.Add(Keyboard.Key.M);
        keys.Add(Keyboard.Key.N);
        keys.Add(Keyboard.Key.O);

        keys.Add(Keyboard.Key.P);
        keys.Add(Keyboard.Key.Q);
        keys.Add(Keyboard.Key.R);

        keys.Add(Keyboard.Key.S);
        keys.Add(Keyboard.Key.T);
        keys.Add(Keyboard.Key.U);

        keys.Add(Keyboard.Key.V);
        keys.Add(Keyboard.Key.W);
        keys.Add(Keyboard.Key.X);

        keys.Add(Keyboard.Key.Y);
        keys.Add(Keyboard.Key.Z);

    }
}

