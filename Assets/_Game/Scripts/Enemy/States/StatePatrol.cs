using System;
using Game.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Game.Enemy.States
{
    public class StatePatrol : MonoBehaviour
    {
        [SerializeField]
        AbilityGun[] _guns;

        [SerializeField]
        float _randomDelayMax = .2f;

        void OnDisable()
        {
            foreach (var gun in _guns)
            {
                gun.SetFiring(false);
                StopAllCoroutines();
            }
        }

        public void PatrolEnter()
        {
            foreach (var gun in _guns)
            {
                float delay = Random.Range(0, _randomDelayMax);
                StartCoroutine(Coroutines.WaitThen(
                    delay,
                    () =>gun.SetFiring(true))
                );
            }
        }
    }
}