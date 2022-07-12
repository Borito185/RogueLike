using System;
using Assets.Code.Util;
using UnityEngine;

namespace Assets.Code.Stats
{
    [Serializable]
    public struct StatValue : INameable
    {
        public StatValue(string name, float baseValue = 0.0f, float multiplier = 1.0f)
        {
            Name = name;
            BaseValue = baseValue;
            Multiplier = multiplier;
        }

        [field: SerializeField]
        public string Name;

        public float BaseValue;

        public float Multiplier;
        public float Value => BaseValue * Multiplier;
        public string GetName() => Name;
    }
}