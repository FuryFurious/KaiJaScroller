using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RandomAction : IActionListener
{
    double chance;

    public RandomAction(double chance)
    {
        this.chance = chance;

    }

    public bool performed(GameTime gameTime, Entity source)
    {
        return Help.random.NextDouble() < chance;
    }
}

