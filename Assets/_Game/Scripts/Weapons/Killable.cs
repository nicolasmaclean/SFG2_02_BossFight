using System;
using Game.Core;
using Game.UI;
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
        [SerializeField]
        string _addToHud = string.Empty;
        
        public UnityEvent<float> OnHurt;
        public UnityEvent<float> OnChange;
        public UnityEvent OnDeath;

        protected virtual void Awake()
        {
            _health = _initialHealth;
        }

        HealthBar _healthBar;
        void Start()
        {
            if (_addToHud == string.Empty) return;
            
            _healthBar = HUDModel.Instance.AddHealthBar(_addToHud);
            OnChange.AddListener(_healthBar.SetHealth);
        }

        void OnDisable()
        {
            if (!_healthBar) return;
            OnChange.RemoveListener(_healthBar.SetHealth);
            HUDModel.Instance.Remove(_healthBar);
            
            _healthBar = null;
        }

        public void Heal(float amount)
        {
            _health += amount;
            OnChange?.Invoke(_health / _initialHealth);
            if (_clampToMaxHealth) _health = Mathf.Min(_health, _initialHealth);
        }

        public void MaxHeal()
        {
            _health = _initialHealth;
            OnChange?.Invoke(_health / _initialHealth);
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
            
            OnHurt?.Invoke(_health / _initialHealth);
            OnChange?.Invoke(_health / _initialHealth);
            return true;
        }

        public void Kill()
        {
            _health = 0;
            
            OnChange?.Invoke(0);
            OnDeath?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}