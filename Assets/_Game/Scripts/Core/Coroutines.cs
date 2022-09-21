﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public static class Coroutines
    {
        public static IEnumerator WaitThen(float seconds, System.Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
        
        public static IEnumerator WaitTill(System.Func<bool> predicate, System.Action callback )
        {
            yield return new WaitUntil(predicate);
            callback?.Invoke();
        }
        
        public static IEnumerator Slider_Lerp(Slider slider, float value, float duration, System.Action callback=null)
        {
            float init = slider.value; 
            float elapsed = 0;

            while (elapsed < duration)
            {
                slider.value = Mathf.Lerp(init, value, elapsed / duration);
                
                yield return null;
                elapsed += Time.deltaTime;
            }

            slider.value = value;
            callback?.Invoke();
            yield break;
        }

        public static IEnumerator Graphic_Lerp(Graphic graphic, Color to, float duration, System.Action callback = null)
        {
            Color init = graphic.color; 
            float elapsed = 0;

            while (elapsed < duration)
            {
                graphic.color = Color.Lerp(init, to, elapsed / duration);
                
                yield return null;
                elapsed += Time.deltaTime;
            }

            graphic.color = to;
            callback?.Invoke();
            yield break;
        }
    }
}