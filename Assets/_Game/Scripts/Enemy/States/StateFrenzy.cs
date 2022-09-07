using System;
using System.Collections;
using Game.Player;
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
        NavMeshAgent _agent;
        Animator _anim;

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _anim = GetComponentInParent<Animator>();
        }

        void Start()
        {
            this.enabled = false;
        }

        void OnEnable()
        {
            _agent.enabled = true;
            _seekCoroutine = StartCoroutine(SeekLoop());
        }

        void OnDisable()
        {
            _agent.enabled = false;
            if (_seekCoroutine != null)
            {
                StopCoroutine(_seekCoroutine);
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

        const float SEEK_INTERVAL = 0.2f;
        IEnumerator SeekLoop()
        {
            while (true)
            {
                _agent.SetDestination(s_parent.position);
                yield return new WaitForSeconds(SEEK_INTERVAL);
            }
        }
    }
}