using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.StateMachine
{
    public class GunController : StateMachineBehaviour
    {
        [SerializeField]
        float _delayMax = 0.4f;
        public void StartFiring(BehaviourData data) => Set(data, true);
        public void StopFiring(BehaviourData data) => Set(data, false);

        Coroutine _delayedStart = null;
        public void DelayedStart(BehaviourData data)
        {
            float amount = Random.Range(0, _delayMax);
            _delayedStart = Coroutines.Start(Coroutines.WaitThen(amount, () =>
            {
                StartFiring(data);
            }));
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_delayedStart != null) Coroutines.Stop(_delayedStart);
        }

        void Set(BehaviourData data, bool value)
        { 
            var guns = data.Animator.GetComponentsInChildren<AbilityGun>();
            foreach (var gun in guns)
            {
                gun.SetFiring(value);
            }
        }
    }
}
