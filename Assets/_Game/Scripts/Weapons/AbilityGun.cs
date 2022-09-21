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
        protected float _speed = 8;

        [SerializeField]
        [Min(0)]
        protected float _damage;

        [SerializeField]
        [ReadOnly]
        protected bool _firing;
        
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
            // ignore redundant event call
            if (context.started) return;

            // set _firing to true when button down
            SetFiring(context.performed);
        }
        
        public virtual bool Fire()
        {
            float elapsedTime = Time.time - _fireTimestamp;
            if (elapsedTime < _fireCooldown) return false;

            _fireTimestamp = Time.time;

            SpawnBullet(_bulletPrefab, _originBullet, _damage, _speed, gameObject.layer);
            
            OnShoot?.Invoke();
            return true;
        }
        
        protected static BulletBasic SpawnBullet(BulletBasic bulletPrefab, Transform origin, float damage, float speed, int layer)
        {
            BulletBasic bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
            bullet.Damage = damage;
            bullet.Speed = speed;
            bullet.gameObject.layer = layer;

            return bullet;
        }

        public virtual void SetFiring(bool value)
        {
            // ignore if _firing does not change
            if (_firing == value) return;
            
            _firing = value;

            if (_firing)
            {
                StartCoroutine(FiringLoop());
            }
        }
        
        IEnumerator FiringLoop()
        {
            while (_firing)
            {
                Fire();
                yield return new WaitForSeconds(_fireCooldown);
            }
        }
    }
}
