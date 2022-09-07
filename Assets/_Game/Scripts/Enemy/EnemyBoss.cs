using System;
using Game.Core;
using Game.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Object = System.Object;

namespace Game.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyBoss : MonoExtended
    {
        [Header("Stats")]
        [SerializeField]
        [ReadOnly]
        Phases _currentPhase = Phases.Spin;

        [SerializeField]
        [ReadOnly]
        bool _inTransition;

        [SerializeField]
        [ReadOnly]
        bool _isDead = false;
        
        Animator _anim;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _inTransition = true;
        }

        public void TriggerPhase2()
        {
            if (_currentPhase != Phases.Spin) return;

            _currentPhase = Phases.Patrol;
            _anim.SetBool(BOOL_BROKE, true);
            _anim.SetTrigger(TRIG_BREAK);
        }

        public void TriggerPhase3()
        {
            if (_currentPhase != Phases.Patrol) return;

            _currentPhase = Phases.Alone;
            _anim.SetBool(BOOL_ALONE, true);
        }
        
        public void SetPhase(string phase)
        {
            switch (phase)
            {
                case "Spin":
                    _currentPhase = Phases.Spin;
                    break;
                case "Patrol":
                    _currentPhase = Phases.Patrol;
                    break;
                case "Alone":
                    _currentPhase = Phases.Alone;
                    break;
            }
        }

        public void SetTransition(int inTransition) => _inTransition = inTransition > 0;
        public void SetDead(int isDead) => _isDead = isDead > 0;
        public void SetEnabledForAnimator(int isEnabled) => _anim.enabled = isEnabled > 0; 
        
        #region Animator Constants
        static readonly int TRIG_BREAK = Animator.StringToHash("Break");
        static readonly int BOOL_BROKE = Animator.StringToHash("Broken");
        static readonly int BOOL_ALONE = Animator.StringToHash("Alone");
        #endregion

        enum Phases
        {
            Spin, Patrol, Alone
        }
    }
}