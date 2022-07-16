using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Skills;
using Mirror;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Code.Stats
{
    [CreateAssetMenu(menuName = "Status/Preset")]
    public class StatusPreset : ScriptableObject
    {
        public List<StatValue> Stats = new();
        public List<DepletableStatValue> DepletableStats = new();

        public List<AssetReferenceT<PassiveSkillBase>> PassiveSkills = new();

        public List<AssetReferenceT<ActiveSkillBase>> ActiveSkills = new();

        [Server]
        public virtual void SetupStatus(Status status)
        {
            status.Stats.AddRange(Stats);
            status.DepletableStats.AddRange(DepletableStats);
            status.PassiveSkills.AddRange(PassiveSkills);

            for (var i = 0; i < ActiveSkills.Count; i++)
            {
                var skill = ActiveSkills[i];
                Status.SkillSlots slot = (Status.SkillSlots)i;
                switch (slot)
                {
                    case Status.SkillSlots.Movement:
                        status.MovementSkill = skill;
                        break;
                    case Status.SkillSlots.First:
                        status.PrimarySkill = skill;
                        break;
                    case Status.SkillSlots.Second:
                        status.SecondarySkill = skill; 
                        break;
                    case Status.SkillSlots.Third:
                        status.TertiarySkill = skill;
                        break;
                }
                status.ActiveSkills.Add(skill);
            }
        }
    }
}