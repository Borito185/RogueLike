using System;
using Assets.Code.Stats;
using Mirror;
using UnityEngine;

namespace Assets.Code.Skills
{
    public abstract class PassiveSkillBase : SkillBase
    {
        [Server]
        public virtual void Use(Status user)
        {
            throw new NotImplementedException($"method:'{nameof(Use)}' should be overriden");
        }
        [Server]
        public virtual void Cancel(Status user)
        {
            throw new NotImplementedException($"method:'{nameof(Cancel)}' should be overriden");
        }
    }
}