using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class ABehavior
{
    protected Entity entity;

    public List<ASkill> skills = new List<ASkill>();

    public void setEntity(Entity e)
    {
        this.entity = e;
    }

    public abstract void init();

    public abstract void update(GameTime gameTime);

    public void updateSkills(GameTime gameTime)
    {
        foreach (ASkill skill in skills)
            skill.update(gameTime);
    }

    public abstract void onKill();
}
