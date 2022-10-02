using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Image))]
    public class Flash : MonoBehaviour
    {
        [SerializeField]
        Color _to;

        [SerializeField]
        float _inTime;

        [SerializeField]
        float _outTime;
        
        [SerializeField]
        Killable _target;

        Image _img;
        Color _from;
        
        void Awake()
        {
            _img = GetComponent<Image>();
            _from = _img.color;
        }

        void OnEnable() => _target.OnHurt.AddListener(FlashImage);
        void OnDisable() => _target.OnHurt.RemoveListener(FlashImage);

        Coroutine _flash;
        void FlashImage(float healthAmt) => FlashImage();
        void FlashImage()
        {
            if (_flash != null) StopCoroutine(_flash);
            _flash = StartCoroutine(Coroutines.Graphic_Color_Lerp(_img, _to, _inTime, () =>
            {
                StartCoroutine(Coroutines.Graphic_Color_Lerp(_img, _from, _outTime));
            }));
        }
    }
}
