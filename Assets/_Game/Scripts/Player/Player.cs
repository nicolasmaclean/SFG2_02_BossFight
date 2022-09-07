using Game.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        public float Health => _health;
        float _health;

        [Header("Stats")]
        [SerializeField]
        float _initialHealth = 10f;

        [Header("Events")]
        public UnityEvent OnHurt;
        public UnityEvent OnDeath;

        void Awake()
        {
            _health = _initialHealth;
        }
        
        public void Hurt(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Kill();
                return;
            }
            
            OnHurt?.Invoke();
        }

        public void Kill()
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}