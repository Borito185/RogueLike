using Assets.Code.Skills;
using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Code.Util
{
    public static class MirrorCustomWriters
    {//may not work
        private static void WriteAssetReferenceT<T>(NetworkWriter writer, AssetReferenceT<T> value) where T : Object
        {
            writer.WriteString(value.AssetGUID);
        }
        private static AssetReferenceT<T> ReadAssetReferenceT<T>(NetworkReader reader) where T : Object
        {
            return new(reader.ReadString());
        }

        public static void WriteSkillBase(this NetworkWriter writer, AssetReferenceT<SkillBase> value) =>
            WriteAssetReferenceT(writer, value);

        public static AssetReferenceT<SkillBase> ReadSkillBase(this NetworkReader reader) =>
            ReadAssetReferenceT<SkillBase>(reader);
        public static void WriteActiveSkillBase(this NetworkWriter writer, AssetReferenceT<ActiveSkillBase> value) =>
            WriteAssetReferenceT(writer, value);

        public static AssetReferenceT<ActiveSkillBase> ReadActiveSkillBase(this NetworkReader reader) =>
            ReadAssetReferenceT<ActiveSkillBase>(reader);
        public static void WritePassiveSkillBase(this NetworkWriter writer, AssetReferenceT<PassiveSkillBase> value) =>
            WriteAssetReferenceT(writer, value);

        public static AssetReferenceT<PassiveSkillBase> ReadPassiveSkillBase(this NetworkReader reader) =>
            ReadAssetReferenceT<PassiveSkillBase>(reader);
    }
}