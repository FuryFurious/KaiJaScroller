using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class KeyboardReader
{
    Input input;

    public String s = "";
    public bool hasFinished = false;


    public KeyboardReader()
    {
        List<Keyboard.Key> keys = new List<Keyboard.Key>();

        Input.getCharKeys(ref keys);
        Input.getNumKeys(ref keys);

        keys.Add(Keyboard.Key.Back);
        keys.Add(Keyboard.Key.Space);
        keys.Add(Keyboard.Key.Return);
        keys.Add(Keyboard.Key.LShift);
        keys.Add(Keyboard.Key.RShift);

        input = new Input(keys);
    }

    public void update()
    {
        char currentChar = ' ';

        input.update();

        if (input.isClicked(Keyboard.Key.A))
            currentChar = 'a';

        else if (input.isClicked(Keyboard.Key.B))
            currentChar = 'b';

        else if (input.isClicked(Keyboard.Key.C))
            currentChar = 'c';

        else if (input.isClicked(Keyboard.Key.D))
            currentChar = 'd';

        else if (input.isClicked(Keyboard.Key.E))
            currentChar = 'e';

        else if (input.isClicked(Keyboard.Key.F))
            currentChar = 'f';

        else if (input.isClicked(Keyboard.Key.G))
            currentChar = 'g';

        else if (input.isClicked(Keyboard.Key.H))
            currentChar = 'h';

        else if (input.isClicked(Keyboard.Key.I))
            currentChar = 'i';

        else if (input.isClicked(Keyboard.Key.J))
            currentChar = 'j';

        else if (input.isClicked(Keyboard.Key.K))
            currentChar = 'k';

        else if (input.isClicked(Keyboard.Key.L))
            currentChar = 'l';

        else if (input.isClicked(Keyboard.Key.M))
            currentChar = 'm';

        else if (input.isClicked(Keyboard.Key.N))
            currentChar = 'n';

        else if (input.isClicked(Keyboard.Key.O))
            currentChar = 'o';

        else if (input.isClicked(Keyboard.Key.P))
            currentChar = 'p';

        else if (input.isClicked(Keyboard.Key.Q))
            currentChar = 'q';

        else if (input.isClicked(Keyboard.Key.R))
            currentChar = 'r';

        else if (input.isClicked(Keyboard.Key.S))
            currentChar = 's';

        else if (input.isClicked(Keyboard.Key.T))
            currentChar = 't';

        else if (input.isClicked(Keyboard.Key.U))
            currentChar = 'u';

        else if (input.isClicked(Keyboard.Key.V))
            currentChar = 'v';

        else if (input.isClicked(Keyboard.Key.W))
            currentChar = 'w';

        else if (input.isClicked(Keyboard.Key.X))
            currentChar = 'x';

        else if (input.isClicked(Keyboard.Key.Y))
            currentChar = 'y';

        else if (input.isClicked(Keyboard.Key.Z))
            currentChar = 'z';

        else if (input.isClicked(Keyboard.Key.Num0))
            currentChar = '0';

        else if (input.isClicked(Keyboard.Key.Num1))
            currentChar = '1';

        else if (input.isClicked(Keyboard.Key.Num2))
            currentChar = '2';

        else if (input.isClicked(Keyboard.Key.Num3))
            currentChar = '3';

        else if (input.isClicked(Keyboard.Key.Num4))
            currentChar = '4';

        else if (input.isClicked(Keyboard.Key.Num5))
            currentChar = '5';

        else if (input.isClicked(Keyboard.Key.Num6))
            currentChar = '6';

        else if (input.isClicked(Keyboard.Key.Num7))
            currentChar = '7';

        else if (input.isClicked(Keyboard.Key.Num8))
            currentChar = '8';

        else if (input.isClicked(Keyboard.Key.Num9))
            currentChar = '9';


        if (input.isClicked(Keyboard.Key.Back) && s.Length > 0)
            s = s.Substring(0, s.Length - 1);

        String newString = currentChar.ToString();

        if (input.isPressed(Keyboard.Key.LShift) || input.isPressed(Keyboard.Key.RShift))
            newString = newString.ToUpper();

        if(currentChar != ' ' || input.isClicked(Keyboard.Key.Space))
            s += newString;

        if (input.isClicked(Keyboard.Key.Return))
            hasFinished = true;
    }


}