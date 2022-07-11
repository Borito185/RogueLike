using System;
using Assets.Code.Stats;
using Mirror;
using UnityEngine;

namespace Assets.Code.Skills
{
    public abstract class ActiveSkillBase : SkillBase
    {
        public double CooldownDuration;
        [Server]
        public virtual SkillCooldown Use(Status user)
        {
            Ability(user);
            return new(CooldownDuration);
        }

        protected virtual void Ability(Status user)
        {
            throw new NotImplementedException($"method:'{nameof(Ability)}' should be overriden");
        }
    }
}