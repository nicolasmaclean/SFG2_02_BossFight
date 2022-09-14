using System;
using Codice.CM.Interfaces;
using Game.Core;
using Game.Weapons;
using UnityEngine;
using UnityEngine.PlayerLoop;
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

        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        void Start() => UpdateVisuals();

        void OnEnable()
        {
            _target.OnChange.AddListener(UpdateVisuals);
        }

        void OnDisable()
        {
            _target.OnChange.RemoveListener(UpdateVisuals);
        }

        Coroutine _update;
        void UpdateVisuals()
        {
            if (_update != null)
            {
                StopCoroutine(_update);
            }
            
            float val = Mathf.Clamp(_target.Health / _target.InitialHealth, 0, 1);
            _update = StartCoroutine(Coroutines.Slider_Lerp(_slider, val, _lerpDuration));
        }
    }
}