using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class CoroutineSource : MonoBehaviour { }
    public static class Coroutines
    {
        static CoroutineSource _source;
        static CoroutineSource s_source
        {
            get
            {
                if (_source == null)
                {
                    _source = new GameObject("Coroutine Source").AddComponent<CoroutineSource>();
                }
                return _source;
            }
        }
        public static Coroutine Start(IEnumerator coroutine) => s_source.StartCoroutine(coroutine);
        public static void Stop(Coroutine coroutine) => s_source.StopCoroutine(coroutine);
        public static void StopAll() => s_source.StopAllCoroutines();

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
        
        public static IEnumerator Image_Fill_Lerp(Image img, float value, float duration, System.Action callback=null)
        {
            float init = img.fillAmount; 
            float elapsed = 0;

            while (elapsed < duration)
            {
                img.fillAmount = Mathf.Lerp(init, value, elapsed / duration);
                
                yield return null;
                elapsed += Time.deltaTime;
            }

            img.fillAmount = value;
            callback?.Invoke();
            yield break;
        }

        public static IEnumerator Graphic_Color_Lerp(Graphic graphic, Color to, float duration, System.Action callback = null)
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

        public static IEnumerator Material_Color_Lerp(
            Material material,
            Color to,
            float duration,
            System.Action callback = null
        )
        {
            Color init = material.color; 
            float elapsed = 0;

            while (elapsed < duration)
            {
                material.color = Color.Lerp(init, to, elapsed / duration);
                
                yield return null;
                elapsed += Time.deltaTime;
            }

            material.color = to;
            callback?.Invoke();
            yield break;
        }
    }
}