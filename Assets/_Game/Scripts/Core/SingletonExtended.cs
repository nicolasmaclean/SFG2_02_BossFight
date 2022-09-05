using UnityEngine;

namespace Game.Core
{
    public class SingletonExtended<T> : MonoExtended where T : MonoExtended
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