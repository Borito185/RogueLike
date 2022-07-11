using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Assets.Code.Util
{
    public static class ListUtil
    {
        public static T FindOfName<T>(this IList<T> list, string name) where T : INameable
        {
            foreach (var item in list)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            Debug.LogWarning($"Couldn't find {name} at {Environment.StackTrace}");
            return default;
        }
        public static T FindOfNameOrCreate<T>(this IList<T> list, string name) where T : INameable, new()
        {
            T value = FindOfName(list, name);
            if (!Equals(value, default(T)))
                return value;
            value = new T();
            list.Add(value);

            return value;
        }
    }
}