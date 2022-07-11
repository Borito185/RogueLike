using System;
using Assets.Code.Stats;
using UnityEngine.UIElements;

namespace Assets.Code.Skills
{
    [Serializable]
    public struct ActiveSkillWrapper
    {
        public ActiveSkillBase Skill;
        public SkillCooldown Cooldown;

        public void Use(Status user)
        {
            if (!Cooldown.CooldownIsOver() || Skill == null)
                return;
            Cooldown = Skill.Use(user);
        }
    }
}