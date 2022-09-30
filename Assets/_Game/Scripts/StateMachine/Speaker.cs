using UnityEngine;
using UnityEngine.Events;

namespace Game.StateMachine
{
    public class Speaker : StateMachineBehaviour
    {
        public UnityEvent<BehaviourData> FirstEnter;
        public UnityEvent<BehaviourData> Enter;
        public UnityEvent<BehaviourData> Update;
        public UnityEvent<BehaviourData> Exit;

        public void DisableAnimator(BehaviourData data)
        {
            data.Animator.enabled = false;
        }

        bool _first = true;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BehaviourData data = new() { Animator = animator, StateInfo = stateInfo, LayerIndex = layerIndex };
            Enter?.Invoke(data);

            if (!_first) return;

            _first = false;
            FirstEnter?.Invoke(data);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BehaviourData data = new() { Animator = animator, StateInfo = stateInfo, LayerIndex = layerIndex };
            Update?.Invoke(data);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BehaviourData data = new() { Animator = animator, StateInfo = stateInfo, LayerIndex = layerIndex };
            Exit?.Invoke(data);
        }
    }

    public class BehaviourData
    {
        public Animator Animator;
        public AnimatorStateInfo StateInfo;
        public int LayerIndex;
    }
}