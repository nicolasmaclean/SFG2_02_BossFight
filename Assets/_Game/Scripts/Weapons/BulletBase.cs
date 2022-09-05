using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BulletBase : MonoExtended
    {
        [Header("Stats")]
        [SerializeField]
        [Min(0)]
        protected float _damage = 0;

        [SerializeField]
        [Min(0)]
        protected float _speed = 5f;

        [SerializeField]
        protected float _lifetime = 30f;

        [Header("Events")]
        public UnityEvent OnHit;
        
        const float DESTROY_DELAY = 0.05f;

        void Start()
        {
            if (_lifetime <= 0) return;
            Destroy(gameObject, _lifetime);
        }
        
        void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            float dist = Time.deltaTime * _speed;
            transform.Translate(0, 0, dist);
        }

        void OnCollisionEnter(Collision collision)
        {
            OnCollision();
        }

        protected virtual void OnCollision()
        {
            OnHit?.Invoke();
            Destroy(gameObject, DESTROY_DELAY);
        }
    }
}
