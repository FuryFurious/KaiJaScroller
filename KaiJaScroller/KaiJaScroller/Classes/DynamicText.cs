using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DynamicText
{

    public Text text = new Text("", Assets.font1);
    public double lifeTime;

    public DynamicText(Vector2f start, String s, double lifeTime)
    {
        this.text.DisplayedString = s;
        this.lifeTime = lifeTime;

        this.text.Scale = new Vector2f(Settings.COMBATTEXTSIZE, Settings.COMBATTEXTSIZE);

        this.text.Position = start + new Vector2f(  (float)Math.Sin(Math.PI * 2 * Help.random.NextDouble()), 
                                                    (float)Math.Sin(Math.PI * 2 * Help.random.NextDouble())) * Settings.COMBATTEXTOFFSET;
    }

    public void update(GameTime gameTime)
    {
        lifeTime -= gameTime.ElapsedTime.TotalSeconds;

        this.text.Position += new Vector2f(0, Settings.COMBATTEXTSPEED);
        this.text.Color = Help.fade(this.text.Color, lifeTime);


    }

    public void draw(GameTime gameTime, RenderTarget target)
    {
        target.Draw(text);
    }
}
