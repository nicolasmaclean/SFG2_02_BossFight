using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletBasic : MonoExtended
    {
        static Transform s_parent;
        
        [Header("Stats")]
        [ReadOnly]
        public float Damage = 0;

        [ReadOnly]
        public float Speed = 5;

        [SerializeField]
        protected float _lifetime = 30f;

        [Header("Events")]
        public UnityEvent OnFire;
        public UnityEvent OnHit;
        
        const float DESTROY_DELAY = 0.05f;

        void Start()
        {
            if (_lifetime > 0)
            {
                Destroy(gameObject, _lifetime);
            }

            if (s_parent == null)
            {
                s_parent = new GameObject("GRP_Bullets").transform;
            }

            transform.parent = s_parent;
            OnFire?.Invoke();
        }
        
        void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            float dist = Time.deltaTime * Speed;
            transform.Translate(0, 0, dist);
        }

        void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke();
            
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            OnCollision(collision, target);
        }

        protected virtual void OnCollision(Collision collision, IDamageable target)
        {
            target?.Hurt(Damage);
            Destroy(gameObject, DESTROY_DELAY);
        }
    }
}
