using System.Diagnostics;
using Assets.Code.Skills;
using Assets.Code.Util;
using Mirror;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor.AddressableAssets.GUI;

namespace Assets.Code.DataManagement
{
    public class AddressablesTest : NetworkBehaviour
    {
        [SyncVar]
        [AssetReferenceUIRestriction]
        public AssetReferenceT<SkillBase> BaseSkill;
        public async void ChangeValue()
        {
            var a = BaseSkill.ToPassiveSkillBase(out var b);
        }
    }
}