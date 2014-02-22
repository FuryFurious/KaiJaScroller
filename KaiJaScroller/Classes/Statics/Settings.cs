using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Settings
{
    //________________________WINDOW STUFF_________________________//
    public const string WINDOWTITLE = "KaiJaScroller";

    public static int windowWidth = 800;
    public static int windowHeight = 600;

    public static int viewportWidth = 400;
    public static int viewportHeight = 300;

    public static Styles windowStyles = Styles.Default;

    //________________________DEBUG STUFF__________________________//
    public static bool drawBoundings        = false;
    public static Color boundingBoxColor    = new Color(255, 0, 0, 127);

    //________________________COMBAT_______________________________//
    public const double INVITIME = 0.4;


    //_______________________COMBAT TEXT___________________//
    public const float COMBATTEXTSPEED  = -0.5f;
    public const float COMBATTEXTOFFSET = 4.0f;
    public const float COMBATTEXTSIZE   = 0.5f;
}

