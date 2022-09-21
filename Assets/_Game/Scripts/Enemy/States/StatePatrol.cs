using System;
using UnityEngine;

namespace Game.Enemy.States
{
    public class StatePatrol : MonoBehaviour
    {
        [SerializeField]
        AbilityGun[] _guns;

        void OnDisable()
        {
            foreach (var gun in _guns)
            {
                gun.SetFiring(false);
            }
        }

        public void PatrolEnter()
        {
            foreach (var gun in _guns)
            {
                gun.SetFiring(true);
            }
        }
    }
}