using Assets.Code.Skills;
using Mirror;
using UnityEditor;
using UnityEngine;

namespace Assets.Code.Util
{
    public static class MirrorCustomWriters
    {//may not work
        public static void WriteSkillBase(this NetworkWriter writer, SkillBase value)
        {
            WriteScriptableObject(writer, value);
        }

        public static SkillBase ReadSkillBase(this NetworkReader reader)
        {
            return ReadScriptableObject<SkillBase>(reader);
        }
        public static void WriteActiveSkillBase(this NetworkWriter writer, ActiveSkillBase value)
        {
            WriteScriptableObject(writer, value);
        }

        public static ActiveSkillBase ReadActiveSkillBase(this NetworkReader reader)
        {
            return ReadScriptableObject<ActiveSkillBase>(reader);
        }
        public static void WritePassiveSkillBase(this NetworkWriter writer, PassiveSkillBase value)
        {
            WriteScriptableObject(writer, value);
        }

        public static PassiveSkillBase ReadPassiveSkillBase(this NetworkReader reader)
        {
            return ReadScriptableObject<PassiveSkillBase>(reader);
        }

        private static void WriteScriptableObject<T>(NetworkWriter writer, T value, string prefix = "Skills") where T : ScriptableObject
        {
            string path = value == null ? "" : prefix + value.name;
            writer.Write(path);
        }
        private static T ReadScriptableObject<T>(NetworkReader reader) where T : ScriptableObject
        {
            string path = reader.ReadString();
            return string.IsNullOrEmpty(path) ? null : Resources.Load<T>(path);
        }
    }
}