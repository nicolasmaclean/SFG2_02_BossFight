using Game.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game.StateMachine
{
    public class DynamicLoop : StateMachineBehaviour
    {
        [Header("Stats")]
        [SerializeField]
        int _initialLoopAmount = 15;

        [SerializeField]
        int _loopDelta = -2;

        [SerializeField]
        [Min(1)]
        int _min = 1;

        [SerializeField]
        [Min(1)]
        int _max = 30;

        [SerializeField]
        [ReadOnly]
        int _currentLoopAmount;

        [Header("Events")]
        public UnityEvent<BehaviourData> OnFirstEnter;
        public UnityEvent<BehaviourData> OnEnter;
        public UnityEvent<BehaviourData> OnExit;

        void Awake()
        {
            _currentLoopAmount = _initialLoopAmount;
        }

        bool _first = true;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // skip the loop clip
            if (_currentLoopAmount == 1)
            {
                animator.SetTrigger(TRIG_EXIT);
            }
            else
            {
                // wait to end loop
                var currentState = animator.GetCurrentAnimatorClipInfo(layerIndex)[0];
                float secondsInLoop = currentState.clip.length * _currentLoopAmount;
                
                Coroutines.Start(Coroutines.WaitThen(secondsInLoop, () =>
                {
                    animator.SetTrigger(TRIG_EXIT);
                }));
            }

            // trigger events
            BehaviourData data = new() { Animator = animator, StateInfo = stateInfo, LayerIndex = layerIndex };
            if (_first)
            {
                OnFirstEnter?.Invoke(data);
                _first = false;
            }
            OnEnter?.Invoke(data);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _currentLoopAmount = Mathf.Clamp(_currentLoopAmount + _loopDelta, _min, _max);
            BehaviourData data = new() { Animator = animator, StateInfo = stateInfo, LayerIndex = layerIndex };
            OnExit?.Invoke(data);
        }
        
        #region Animator Constants
        static readonly int TRIG_EXIT = Animator.StringToHash("SBMDynamicLoop_Exit");
        #endregion
    }
}