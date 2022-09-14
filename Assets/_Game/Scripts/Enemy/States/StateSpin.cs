using System;
using System.Collections;
using Game.Core;
using Game.Weapons;
using UnityEngine;

namespace Game.Enemy.States
{
    [RequireComponent(typeof(Animator))]
    public class StateSpin : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        int _initialRevolutionsPerDirection = 15;

        [SerializeField]
        int _revolutionDecrement = 2;
        
        [SerializeField]
        [ReadOnly]
        int _revolutionsPerDirection;
        
        [SerializeField]
        [ReadOnly]
        int _revolutions;

        [Header("Attack")]
        [SerializeField]
        AbilityGun _gun;

        [SerializeField]
        [ReadOnly]
        bool _firing;
        
        Animator _anim;

        #region MonoBehaviour
        void Awake()
        {
            _anim = GetComponent<Animator>();
            _revolutionsPerDirection = _initialRevolutionsPerDirection;
        }

        void OnDisable()
        {
            if (_fireLoop != null) StopCoroutine(_fireLoop);
            _firing = false;
        }
        #endregion

        #region Animation Events
        public void SpinEnter()
        {
            // skip linear loop if we are down to single revolutions
            if (_revolutionsPerDirection == 1)
            {
                _anim.SetTrigger(TRIG_SKIPLOOP);
            }

            _revolutions = 1;
            if (!_firing) StartFiring();
        }

        public void SpinIteration()
        {
            _revolutions++;
            
            if (_revolutions >= _revolutionsPerDirection)
            {
                _anim.SetTrigger(TRIG_SKIPLOOP);
            }
        }

        public void SpinExit(int isCounterClockwise)
        {
            if (isCounterClockwise < 0)
            {
                _revolutionsPerDirection = Mathf.Max(_revolutionsPerDirection - _revolutionDecrement, 1);
            }
        }
        #endregion

        Coroutine _fireLoop;
        void StartFiring()
        {
            _firing = true;
            _fireLoop = StartCoroutine(FireLoop());

            IEnumerator FireLoop()
            {
                while (_firing)
                {
                    yield return null;
                    _gun.Fire();
                }
            }
        }

        #region Animator Constants
        static readonly int TRIG_SKIPLOOP = Animator.StringToHash("ExitLoop");
        #endregion
    }
}