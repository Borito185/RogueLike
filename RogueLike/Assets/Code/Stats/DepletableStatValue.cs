using System;
using Assets.Code.Util;
using UnityEngine;

namespace Assets.Code.Stats
{
    [Serializable]
    public struct DepletableStatValue : INameable
    {
        public string Name;
        public StatValue CurrentValue { get; }
        public StatValue MaxValue { get; }

        public DepletableStatValue(string name)
        {
            Name = name;
            MaxValue = new(name + "_max");
            CurrentValue = new(name + "_current");
        }

        public string GetName() => Name;
    }
}