using System;
using System.Collections;
using Game.Core;
using Game.Player;
using Game.Utils;
using Game.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.States
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateFrenzy : MonoBehaviour
    {
        static Transform _player;
        static Transform s_parent
        {
            get
            {
                if (_player == null)
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
        Animator _anim;
        ColliderMessager[] _hitboxes;

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _anim = GetComponentInParent<Animator>();
            _hitboxes = GetComponentsInChildren<ColliderMessager>();
        }

        void Start()
        {
            this.enabled = false;
        }

        void OnEnable()
        {
            _agent.enabled = true;
            _seekCoroutine = StartCoroutine(SeekLoop());

            foreach (var hitbox in _hitboxes)
            {
                hitbox.CollisionEnter += HitPlayer;
            }
        }

        void OnDisable()
        {
            _agent.enabled = false;
            if (_seekCoroutine != null)
            {
                StopCoroutine(_seekCoroutine);
            }

            foreach (var hitbox in _hitboxes)
            {
                hitbox.CollisionEnter += HitPlayer;
            }
        }

        Coroutine _seekCoroutine;
        public void StartFrenzy()
        {
            // we will no longer be using the animator
            _anim.enabled = false;
            
            // start the loop
            this.enabled = true;
        }

        void HitPlayer(Collision collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            
            // ignore non-player collisions
            if (player == null) return;

            // apply knockback and damage to player
            KillableKnockback knockback = player.GetComponent<KillableKnockback>();
            if (knockback)
            {
                knockback.HurtWithKnockback(_damage, transform.position);
            }
            
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
                if (!_stunned) _agent.SetDestination(s_parent.position);
                yield return new WaitForSeconds(SEEK_INTERVAL);
            }
        }
    }
}