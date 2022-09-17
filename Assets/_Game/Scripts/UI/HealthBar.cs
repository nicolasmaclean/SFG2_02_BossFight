using Game.Core;
using Game.Utils;
using Game.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Game._Game.Scripts.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        Killable _target;

        [SerializeField]
        float _lerpDuration = .1f;

        Slider _slider;
        IStoppable[] _stoppables;

        void Awake()
        {
            _slider = GetComponent<Slider>();
            _stoppables = GetComponentsInChildren<IStoppable>();
        }

        void Start() => UpdateVisuals();

        void OnEnable()
        {
            _target.OnChange.AddListener(UpdateVisuals);
        }

        void OnDisable()
        {
            _target.OnChange.RemoveListener(UpdateVisuals);
            if (_update != null)
            {
                StopCoroutine(_update);
            }
        }

        Coroutine _update;
        void UpdateVisuals()
        {
            if (_update != null)
            {
                StopCoroutine(_update);
            }
            
            foreach (var s in _stoppables)
            {
                s.Start();
            }
            
            float val = Mathf.Clamp(_target.Health / _target.InitialHealth, 0, 1);
            _update = StartCoroutine(Coroutines.Slider_Lerp(_slider, val, _lerpDuration,
            () =>
            {
                foreach (var s in _stoppables)
                {
                    s.Stop();
                }
            }));
        }
    }
}