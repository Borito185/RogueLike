using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Code.Skills;
using Assets.Code.Util;
using Mirror;
using Steamworks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Code.Stats
{
    public class Status : NetworkBehaviour, INameable
    {
        [SerializeField]
        private AssetReferenceT<StatusPreset> Preset;

        [SyncVar]
        public string Name;
        public string GetName() => Name;


        public readonly SyncList<DepletableStatValue> DepletableStats = new();
        public readonly SyncList<StatValue> Stats = new();
        public readonly SyncList<AssetReferenceT<ActiveSkillBase>> ActiveSkills = new();
        public readonly SyncList<AssetReferenceT<PassiveSkillBase>> PassiveSkills = new();

        //the string is the assetguid (stored in assetreference) to skill
        public readonly SyncDictionary<string, SkillCooldown> SkillCooldowns = new();

        [SyncVar]
        public AssetReferenceT<ActiveSkillBase> MovementSkill;
        [SyncVar]
        public AssetReferenceT<ActiveSkillBase> PrimarySkill;
        [SyncVar]
        public AssetReferenceT<ActiveSkillBase> SecondarySkill;
        [SyncVar]
        public AssetReferenceT<ActiveSkillBase> TertiarySkill;

        
        [Command]
        public async void UseSkill(SkillSlots skillSlot)
        {
            AssetReferenceT<ActiveSkillBase> skill = skillSlot switch
            {
                SkillSlots.Movement => MovementSkill,
                SkillSlots.First => PrimarySkill,
                SkillSlots.Second => SecondarySkill,
                SkillSlots.Third => TertiarySkill,
                _ => null
            };
            if (skill == null)
                return;
            await UseSkill(skill);
        }

        [Server]
        private async Task UseSkill(AssetReferenceT<ActiveSkillBase> skillReference)
        {
            if (SkillCooldowns.TryGetValue(skillReference.AssetGUID, out SkillCooldown cooldown) && !cooldown.CooldownIsOver())
                return;

            ActiveSkillBase skill = await skillReference.GetAsset();
            cooldown = skill.Use(this);
            SkillCooldowns[skillReference.AssetGUID] = cooldown;
        }


        /// <summary>
        /// init status in OnStartServer, else clients wouldn't sync
        /// </summary>
        public override void OnStartLocalPlayer()
        {
           // CmdInitialize();
        }

        [Command]
        public async void CmdInitialize()
        {
            var preset = await Preset.GetAsset();

            if (preset != null)
            {
                preset.SetupStatus(this);
            }

            Preset.ReleaseAsset();
        }
        public enum SkillSlots : byte
        {
            Movement,
            First,
            Second,
            Third
        }
    }
}
