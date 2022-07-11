using UnityEngine;

namespace Assets.Code
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        public virtual void Awake()
        {
            T instance = GetComponent<T>();

            if (!Equals(Instance, default(T)))
            {
                Destroy(instance);
                return;
            }
            Instance = instance;
        }

        public void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public static bool TryGetInstance(out T instance)
        {
            instance = Instance;
            return instance != null;
        }
    }
}