using System.Threading.Tasks;
using Assets.Code.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Code.Util
{
    public static class AssetReferenceUtil
    {
        public static async Task<T> GetAsset<T>(this AssetReferenceT<T> reference) where T : Object
        {
            return reference.Asset == null ? await reference.LoadAssetAsync().Task : reference.Asset as T;
        }

        public static bool ToActiveSkillBase(this AssetReferenceT<SkillBase> skill, out AssetReferenceT<ActiveSkillBase> activeSkill)
        {
            activeSkill = new(skill.AssetGUID);

            return true;
        }
        public static bool ToPassiveSkillBase(this AssetReferenceT<SkillBase> skill, out AssetReferenceT<PassiveSkillBase> activeSkill)
        {
            activeSkill = new(skill.AssetGUID);

            return true;
        }
    }
}