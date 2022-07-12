using System.Collections.Generic;
using Assets.Code.Skills;
using Assets.Code.Util;
using Mirror;
using UnityEditor;
using UnityEngine;

namespace Assets.Code.Stats
{
    public class Status : NetworkBehaviour, INameable
    {
        [SerializeField]
        private StatusPreset Preset;

        [SyncVar]
        public string Name;
        public string GetName() => Name;


        public readonly SyncList<DepletableStatValue> DepletableStats = new();
        public readonly SyncList<StatValue> Stats = new();
        public readonly SyncList<SkillBase> Skills = new();

        [SyncVar]
        public ActiveSkillWrapper MovementSkill;
        [SyncVar]
        public ActiveSkillWrapper PrimarySkill;
        [SyncVar]
        public ActiveSkillWrapper SecondarySkill;
        [SyncVar]
        public ActiveSkillWrapper TertiarySkill;

        [Command]
        public void UseSkill(int skillSlot)
        {
            switch (skillSlot)
            {
                case 0:
                    MovementSkill.Use(this); ;
                    break;
                case 1:
                    PrimarySkill.Use(this); ;
                    break;
                case 2:
                    SecondarySkill.Use(this); ;
                    break;
                case 3:
                    TertiarySkill.Use(this); ;
                    break;
            }
        }

        /// <summary>
        /// init status in OnStartServer, else clients wouldn't sync
        /// </summary>
        public override void OnStartLocalPlayer()
        {
            cmdInitializeFromPreset();
        }

        [Command]
        public void cmdInitializeFromPreset()
        {
            InitializeFromPreset(Preset);
        }
        /// <summary>
        /// Resets the status and initializes it according to given preset
        /// </summary>
        /// <param name="preset">preset to init from</param>
        public virtual void InitializeFromPreset(StatusPreset preset)
        {
            if (preset == null)
                return;

            ResetStatus();

            Stats.AddRange(preset.Stats);
            DepletableStats.AddRange(preset.DepletableStats);
            Skills.AddRange(preset.Skills);
        }
        /// <summary>
        /// clears Stats DepletableStats and Skills SyncLists
        /// </summary>
        public void ResetStatus()
        {
            Stats.Clear();
            DepletableStats.Clear();
            Skills.Clear();
        }
    }
}
