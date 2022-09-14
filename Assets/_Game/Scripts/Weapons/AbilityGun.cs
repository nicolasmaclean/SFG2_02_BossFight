using System.Collections;
using System.Collections.Generic;
using Game.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game
{
    public class AbilityGun : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField]
        [Min(0)]
        float _fireCooldown = .3f;

        [SerializeField]
        [Min(0)]
        float _damage;
        
        [Header("References")]
        [SerializeField]
        Transform _originBullet;

        [SerializeField]
        BulletBasic _bulletPrefab;
        
        [Header("Events")]
        public UnityEvent OnShoot;
        
        float _fireTimestamp;
        
        public void FireEvent(InputAction.CallbackContext context)
        {
            // activate on button down
            if (!context.performed) return;
            
            Fire();
        }
        
        void Fire()
        {
            float elapsedTime = Time.time - _fireTimestamp;
            if (elapsedTime < _fireCooldown) return;

            _fireTimestamp = Time.time;
            
            BulletBasic bullet = Instantiate(_bulletPrefab, _originBullet.position, _originBullet.rotation);
            bullet.Damage = _damage;
            bullet.gameObject.layer = gameObject.layer;
            
            OnShoot?.Invoke();
        }
    }
}
