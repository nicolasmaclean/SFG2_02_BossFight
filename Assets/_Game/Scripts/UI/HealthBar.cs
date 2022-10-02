using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Data")]
        public string TargetName = string.Empty;
        
        [SerializeField]
        [Range(0, 1)]
        float _fraction = 1f;
        public float Fraction
        {
            set
            {
                _fraction = Mathf.Clamp(value, 0, 1);
                UpdateBar();
            }
        }
        public void SetHealth(float fraction) => Fraction = fraction;

        [FormerlySerializedAs("_leftAligned")] public bool LeftAligned = true;

        [Header("Animation")]
        [SerializeField]
        float _lerpDelay = 0.2f;
        
        [SerializeField]
        float _lerpDuration = 0.1f;
        public float LerpLength => _lerpDelay + _lerpDuration;

        [Header("References")]
        [SerializeField, ReadOnly]
        TMP_Text _name;
        
        [SerializeField]
        Image _bar;
        
        [SerializeField]
        TMP_Text _nameLeft;
        
        [SerializeField]
        TMP_Text _nameRight;

        [Space]
        public UnityEvent<float> OnUpdate;

        void Start()
        {
            Configure();
        }

        #if UNITY_EDITOR
        void OnValidate()
        {
            Configure();
        }
        #endif

        public void Configure()
        {
            UpdateBar(false);
            if (TargetName == string.Empty) return;
            
            _bar.fillOrigin = LeftAligned ? 0 : 1;
            if (LeftAligned)
            {
                _nameRight.gameObject.SetActive(false);
                _nameLeft.gameObject.SetActive(true);

                _name = _nameLeft;
            }
            else
            {
                _nameLeft.gameObject.SetActive(false);
                _nameRight.gameObject.SetActive(true);
                _name = _nameRight;
            }
            
            _name.text = TargetName;
        }

        Coroutine _healthAnimation = null;
        void UpdateBar(bool animated=true)
        {
            // cancel animation, if already going
            // we are going to restart it
            if (_healthAnimation != null) StopCoroutine(_healthAnimation);
            
            // instant update
            if (!animated)
            {
                _bar.fillAmount = _fraction;
                return;
            }

            _healthAnimation = StartCoroutine(Coroutines.WaitThen(_lerpDelay, () =>
            {
                StartCoroutine(Coroutines.Image_Fill_Lerp(_bar, _fraction, _lerpDuration));
            }));
            OnUpdate?.Invoke(_fraction);
        }
    }
}