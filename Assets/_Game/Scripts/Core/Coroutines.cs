using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public static class Coroutines
    {
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
    }
}