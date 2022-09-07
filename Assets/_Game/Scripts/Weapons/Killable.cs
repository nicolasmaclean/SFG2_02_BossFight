using Game.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons
{
    public class Killable : MonoExtended, IDamageable
    {
        public float Health => _health;
        float _health;

        [Header("Stats")]
        [SerializeField]
        float _initialHealth = 10f;

        [SerializeField]
        bool _clampToMaxHealth = true; 

        [Header("Events")]
        public UnityEvent OnHurt;
        public UnityEvent OnDeath;

        void Awake()
        {
            _health = _initialHealth;
        }

        public void Heal(float amount)
        {
            _health += amount;
            if (_clampToMaxHealth) _health = Mathf.Min(_health, _initialHealth);
        }

        public void MaxHeal()
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