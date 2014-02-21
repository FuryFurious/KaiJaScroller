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

    

    Vector2f oldStickLeftPos;
    Vector2f currentStickLeftPos;
    Vector2f oldStickRightPos;
    Vector2f currentStickRightPos;
    float oldTriggerPos;
    float currentTriggerPos;

    float x;
    float y;

    public Gamepad()
    {
        oldButtons = new bool[10];
        currentButtons = new bool[10];
    }

    public void update()
    {
        Joystick.Update();
        x = Joystick.GetAxisPosition(0, Joystick.Axis.X);
        y = Joystick.GetAxisPosition(0, Joystick.Axis.Y);

        /*
        Console.Clear();

        for (uint i = 0; i < 10; i++)
        {
            Console.WriteLine(i + ", " + Joystick.IsButtonPressed(0, i));
        }
        Console.WriteLine(Joystick.GetAxisPosition(0, Joystick.Axis.X));
        Console.WriteLine(Joystick.GetAxisPosition(0, Joystick.Axis.Y));
        Console.WriteLine(Joystick.GetAxisPosition(0, Joystick.Axis.R));
        Console.WriteLine(Joystick.GetAxisPosition(0, Joystick.Axis.U));
        */
        oldStickLeftPos = currentStickLeftPos;
        currentStickLeftPos = new Vector2f(Joystick.GetAxisPosition(0,Joystick.Axis.X),Joystick.GetAxisPosition(0,Joystick.Axis.Y)) ;

        oldStickRightPos = currentStickRightPos;
        currentStickRightPos = new Vector2f(Joystick.GetAxisPosition(0, Joystick.Axis.U), Joystick.GetAxisPosition(0, Joystick.Axis.R));
        oldTriggerPos = currentTriggerPos;
        currentTriggerPos = Joystick.GetAxisPosition(0, Joystick.Axis.Z);

        for (int i = 0; i < oldButtons.Length; i++)
            oldButtons[i] = currentButtons[i];

        for (uint i = 0; i < oldButtons.Length; i++)
            currentButtons[i] = Joystick.IsButtonPressed(0, i);
    }
    
    public bool isClicked(uint button)
    {
        return currentButtons[button] && !oldButtons[button];
    }

    public bool isPressed(uint button)
    {
        return Joystick.IsButtonPressed(0, button);
    }

    public bool isReleased(uint button)
    {
        return oldButtons[button] && !Joystick.IsButtonPressed(0, button);
    }
    public float getLeftX()
    {
        return currentStickLeftPos.X;
    }
    public float getLeftY()
    {
        return currentStickLeftPos.Y;
    }

}


