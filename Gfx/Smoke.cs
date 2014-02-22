using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace CandySlayerV2
{
    class Smoke : Particle
    {
        double lastPulse;
        Rectangle source;
        Color color;
        Vector2 start;
 

        public Smoke(Vector2 start, double lifeTime)
        {
            this.pos = start;
            this.start = start;
            this.color = Color.White;
            help = 0;
            

            lastPulse = Game1.gameTime.TotalGameTime.TotalSeconds;


            dir = Vector2.Lerp(new Vector2(0.5f,-1), new Vector2(-0.5f, -1), (float)Other.random.NextDouble());

            speed = 1.5f;

            size = Math.Max(1.5f * (float)Other.random.NextDouble(), 0.5f);
            rotation = (float)(2 * Math.PI * Other.random.NextDouble());

            currentLifetime = lifeTime;

            help = Other.random.Next(0, 5);

            source = new Rectangle(0, 0, 64, 64);

        }

        public override void update()
        {
            currentLifetime -= Game1.gameTime.ElapsedGameTime.TotalSeconds;
        
            pos = pos + speed * dir;

            rotation -= 0.05f;
        }

        public override void draw()
        {
            Game1.spriteBatch.Draw(Res.smoke, pos, source, color * Math.Min((float)currentLifetime, 1), rotation, new Vector2(32, 32), size, SpriteEffects.None, 0);
        }
    }
}
