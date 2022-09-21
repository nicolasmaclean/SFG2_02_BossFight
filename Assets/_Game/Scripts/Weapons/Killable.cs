using Game.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons
{
    public class Killable : MonoExtended, IDamageable
    {
        public bool Invincible;
        
        [SerializeField]
        [ReadOnly]
        float _health;
        public float Health => _health;

        [Header("Stats")]
        [SerializeField]
        float _initialHealth = 10f;
        public float InitialHealth => _initialHealth;

        [SerializeField]
        bool _clampToMaxHealth = true; 

        [Header("Events")]
        public UnityEvent OnHurt;
        public UnityEvent OnDeath;
        public UnityEvent OnChange;

        protected virtual void Awake()
        {
            _health = _initialHealth;
        }

        public void Heal(float amount)
        {
            _health += amount;
            OnChange?.Invoke();
            if (_clampToMaxHealth) _health = Mathf.Min(_health, _initialHealth);
        }

        public void MaxHeal()
        {
            _health = _initialHealth;
            OnChange?.Invoke();
        }
        
        public bool Hurt(float damage)
        {
            if (Invincible) return false;
            
            _health -= damage;
            if (_health <= 0)
            {
                Kill();
                return false;
            }
            
            OnHurt?.Invoke();
            OnChange?.Invoke();
            return true;
        }

        public void Kill()
        {
            _health = 0;
            
            OnDeath?.Invoke();
            OnChange?.Invoke();
            
            Destroy(gameObject);
        }
    }
}