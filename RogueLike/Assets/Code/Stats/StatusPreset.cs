using System.Collections.Generic;
using Assets.Code.Skills;
using UnityEngine;

namespace Assets.Code.Stats
{
    [CreateAssetMenu(menuName = "Status/Preset")]
    public class StatusPreset : ScriptableObject
    {
        public List<StatValue> Stats = new();
        public List<DepletableStatValue> DepletableStats = new();
        public List<SkillBase> Skills = new();
    }
}