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
        AnimationCurve _cooldownCurve = AnimationCurve.Linear(0, .4f, 1, .15f);

        [SerializeField]
        Transform[] _guns;

        [SerializeField]
        BulletBase _bulletPrefab;

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
                    yield return new WaitForSeconds(SampleCooldown());
                    Fire();        
                }
            }
        }

        float SampleCooldown()
        {
            float t = 1 - (float) (_revolutionsPerDirection - 1) / (_initialRevolutionsPerDirection - 1);
            return _cooldownCurve.Evaluate(t);
        }

        void Fire()
        {
            foreach (var gun in _guns)
            {
                GameObject go = Instantiate(_bulletPrefab, gun.position, gun.rotation).gameObject;
                go.layer = gameObject.layer;
            }
        }

        #region Animator Constants
        static readonly int TRIG_SKIPLOOP = Animator.StringToHash("ExitLoop");
        #endregion
    }
}