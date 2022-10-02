using System;
using System.Collections;
using Game.Core;
using Game.Player;
using Game.Utils;
using Game.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateFrenzy : MonoBehaviour
    {
        static Transform _player;
        static Transform s_parent
        {
            get
            {
                if (_player == null && PlayerController.Instance != null)
                {
                    _player = PlayerController.Instance.transform;
                }

                return _player;
            }
        }

        [SerializeField]
        float _damage = 0;
        
        [SerializeField]
        float _stunTime = 2f;

        [SerializeField]
        [ReadOnly]
        bool _stunned;
        
        NavMeshAgent _agent;
        ColliderMessager[] _hitboxes;

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _hitboxes = GetComponentsInChildren<ColliderMessager>();
        }

        void Start()
        {
            this.enabled = false;
        }

        void OnEnable()
        {
            _agent.enabled = true;
            StartCoroutine(SeekLoop());

            foreach (var hitbox in _hitboxes)
            {
                hitbox.CollisionEnter += HitPlayer;
            }
        }

        void OnDisable()
        {
            _agent.enabled = false;
            foreach (var hitbox in _hitboxes)
            {
                hitbox.CollisionEnter -= HitPlayer;
            }
        }

        void HitPlayer(Collision collision)
        {
            GameObject other = collision.gameObject;
            IDamageable dmg = other.GetComponent<IDamageable>();
            Knockbackable kb = other.GetComponent<Knockbackable>();

            // ignore if no-op
            if (dmg == null && kb == null) return;
            
            // apply damage to player
            dmg?.Hurt(_damage);
            
            // apply knockback
            if (kb) kb.Apply(transform.position);
            
            // stun the boss
            _stunned = true;
            StartCoroutine(Coroutines.WaitThen(_stunTime, () =>
            {
                _stunned = false;
            }));
        }

        const float SEEK_INTERVAL = 0.2f;
        IEnumerator SeekLoop()
        {
            while (true)
            {
                // stop the loop, if this component has been disabled
                if (!this.enabled) yield break;
                
                if (!_stunned) _agent.SetDestination(s_parent.position);
                yield return new WaitForSeconds(SEEK_INTERVAL);
            }
        }
    }
}