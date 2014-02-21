using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Gamepad
{
    
    

    bool[] oldButtons;
    bool[] currentButtons;

    bool[] oldStick;
    bool[] currentStick;

    Vector2i oldStickPos;
    Vector2i currentMousePos;

    float x;
    float y;

    public Gamepad()
    {


        oldButtons = new bool[(int)Keyboard.Key.KeyCount];
        currentButtons = new bool[(int)Keyboard.Key.KeyCount];

        oldStick = new bool[(int)Mouse.Button.ButtonCount];
        currentStick = new bool[(int)Mouse.Button.ButtonCount];
    }


    public void update()
    {
        Joystick.Update();
        x = Joystick.GetAxisPosition(0, Joystick.Axis.X);
        y = Joystick.GetAxisPosition(0, Joystick.Axis.Y);

        
        oldStickPos = currentMousePos;
        currentMousePos = Mouse.GetPosition();

        

       



        

        for (int i = 0; i < oldStick.Length; i++)
            oldStick[i] = currentStick[i];

        //update every mouseKey:
        for (int i = 0; i < oldStick.Length; i++)
            if (Mouse.IsButtonPressed((Mouse.Button)i))
                currentStick[i] = true;
            else
                currentStick[i] = false;


    }

    public Vector2f getDeltaMousePos()
    {
        return new Vector2f(currentMousePos.X - oldStickPos.X, currentMousePos.Y - oldStickPos.Y);
    }

    public bool isClicked(Keyboard.Key key)
    {
        return currentButtons[(int)key] && !oldButtons[(int)key];
    }

    public bool isPressed(Keyboard.Key key)
    {
        return Keyboard.IsKeyPressed(key);
    }

    public bool isReleased(Keyboard.Key key)
    {
        return oldButtons[(int)key] && !Keyboard.IsKeyPressed(key);
    }



    public bool leftClicked()
    {
        return !oldStick[(int)Mouse.Button.Left] && currentStick[(int)Mouse.Button.Left];
    }

    public bool leftPressed()
    {
        return currentStick[(int)Mouse.Button.Left];
    }

    public bool leftReleased()
    {
        return oldStick[(int)Mouse.Button.Left] && !currentStick[(int)Mouse.Button.Left];
    }




    public bool rightClicked()
    {
        return !oldStick[(int)Mouse.Button.Right] && currentStick[(int)Mouse.Button.Right];
    }

    public bool rightPressed()
    {
        return currentStick[(int)Mouse.Button.Right];
    }

    public bool rightReleased()
    {
        return oldStick[(int)Mouse.Button.Right] && !currentStick[(int)Mouse.Button.Right];
    }


    public bool midClicked()
    {
        return !oldStick[(int)Mouse.Button.Middle] && currentStick[(int)Mouse.Button.Middle];
    }

    public bool midPressed()
    {
        return currentStick[(int)Mouse.Button.Middle];
    }

    public bool midReleased()
    {
        return oldStick[(int)Mouse.Button.Middle] && !currentStick[(int)Mouse.Button.Middle];
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

