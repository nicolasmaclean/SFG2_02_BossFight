using UnityEngine;

namespace Game.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            OnAwake();
        }
        
        protected virtual void OnAwake() { }
    }
}