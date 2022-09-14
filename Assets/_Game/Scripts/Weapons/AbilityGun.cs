using System.Collections;
using System.Collections.Generic;
using Game.Core;
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
        protected float _damage;
        
        [Header("References")]
        [SerializeField]
        protected Transform _originBullet;

        [SerializeField]
        protected BulletBasic _bulletPrefab;
        
        [Header("Events")]
        public UnityEvent OnShoot;
        
        float _fireTimestamp;
        
        public void FireEvent(InputAction.CallbackContext context)
        {
            // activate on button down
            if (!context.performed) return;
            
            Fire();
        }
        
        public virtual bool Fire()
        {
            float elapsedTime = Time.time - _fireTimestamp;
            if (elapsedTime < _fireCooldown) return false;

            _fireTimestamp = Time.time;

            SpawnBullet(_bulletPrefab, _originBullet, _damage, gameObject.layer);
            
            OnShoot?.Invoke();
            return true;
        }

        protected static BulletBasic SpawnBullet(BulletBasic bulletPrefab, Transform origin, float damage, int layer)
        {
            BulletBasic bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
            bullet.Damage = damage;
            bullet.gameObject.layer = layer;

            return bullet;
        }
    }
}
