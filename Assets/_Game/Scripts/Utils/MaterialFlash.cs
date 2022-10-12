using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialFlash : MonoBehaviour
    {
        [SerializeField]
        Color _toColor = Color.red;

        [SerializeField]
        float _inLength = .05f;

        [SerializeField]
        float _holdLength = .1f;
        
        [SerializeField]
        float _outLength = .1f;
        
        Material _mat;
        Color _initialColor;
        Coroutine _flashing;

        void Awake()
        {
            var ren = GetComponent<Renderer>();
            _mat = ren.material;
            _initialColor = _mat.color;
        }

        public void In()
        {
            if (_flashing != null) StopCoroutine(_flashing);

            _flashing = StartCoroutine(Coroutines.Material_Color_Lerp(_mat, _toColor, _inLength, () => _flashing = null));
        }

        public void Out()
        {
            if (_flashing != null) StopCoroutine(_flashing);
            
            _flashing = StartCoroutine(Coroutines.Material_Color_Lerp(_mat, _initialColor, _outLength, () => _flashing = null));
        }

        public void InAndOut()
        {
            if (_flashing != null) StopCoroutine(_flashing);

            _flashing = StartCoroutine(Flash());
            IEnumerator Flash()
            {
                yield return Coroutines.Material_Color_Lerp(_mat, _toColor, _inLength);
                yield return new WaitForSeconds(_holdLength);
                yield return Coroutines.Material_Color_Lerp(_mat, _initialColor, _outLength);
                _flashing = null;
                yield break;
            }
        }
    }
}
