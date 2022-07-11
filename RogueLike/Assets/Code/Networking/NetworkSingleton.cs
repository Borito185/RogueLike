using Mirror;

namespace Assets.Code.Networking
{
    public class NetworkSingleton<T> : NetworkBehaviour where T : NetworkBehaviour
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
    }
}