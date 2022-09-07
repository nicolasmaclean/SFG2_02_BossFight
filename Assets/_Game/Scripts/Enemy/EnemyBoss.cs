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
        Phases _currentPhase = Phases.Pre_Spin;
                
        [SerializeField]
        [Core.ReadOnly]
        protected float _health;
        public float Health => _health;
        
        [SerializeField]
        [Core.ReadOnly]
        protected float _healthLeft;
        public float HealthLeft => _healthLeft;

        [SerializeField]
        public float _initialHealth = 1f;

        [Header("Events")]
        public UnityEvent OnHurt;
        public UnityEvent OnDeath;
        
        public UnityEvent OnPhase1End;
        public UnityEvent OnPhase2End;
        
        Animator _anim;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _health = _initialHealth;
        }

        public void Hurt(float damage, bool isLeft = false)
        {
            if (_currentPhase != Phases.Spin && _currentPhase != Phases.Patrol && _currentPhase != Phases.Alone) return;

            if (_currentPhase == Phases.Patrol && isLeft)
            {
                _healthLeft -= damage;
            }
            else
            {
                _health -= damage;
            }
            
            if (Health <= 0)
            {
                switch (_currentPhase)
                {
                    // end of spin phase
                    case Phases.Spin:
                        _anim.SetBool(BOOL_BROKE, true);
                        _anim.SetTrigger(TRIG_BREAK);

                        _health = _healthLeft = _initialHealth / 2;
                        OnPhase1End?.Invoke();
                        break;
                    
                    // end of patrol phase
                    case Phases.Patrol:
                        _anim.SetBool(BOOL_ALONE, true);
                        
                        _health = _initialHealth;
                        OnPhase2End?.Invoke();
                        break;
                    
                    // DEATH :Skull:
                    default:
                        Kill();
                        break;
                }
                
                return;
            }

            OnHurt?.Invoke();
        }
        
        public void Kill()
        {
            _health = 0;
            OnDeath?.Invoke();
            Destroy(gameObject, 0.01f);
        }

        public void SetPhase(string phase)
        {
            switch (phase)
            {
                case "Pre_Spin":
                    _currentPhase = Phases.Pre_Spin;
                    break;
                case "Spin":
                    _currentPhase = Phases.Spin;
                    break;
                case "Pre_Patrol":
                    _currentPhase = Phases.Pre_Patrol;
                    break;
                case "Patrol":
                    _currentPhase = Phases.Patrol;
                    break;
                case "Pre_Alone":
                    _currentPhase = Phases.Pre_Alone;
                    break;
                case "Alone":
                    _currentPhase = Phases.Alone;
                    break;
                case "Dead":
                    _currentPhase = Phases.Dead;
                    break;
            }
        }
        
        #region Animator Constants
        static readonly int TRIG_BREAK = Animator.StringToHash("Break");
        static readonly int BOOL_BROKE = Animator.StringToHash("Broken");
        static readonly int BOOL_ALONE = Animator.StringToHash("Alone");
        #endregion

        enum Phases
        {
            Pre_Spin, Spin, Pre_Patrol, Patrol, Pre_Alone, Alone, Dead
        }
    }
}