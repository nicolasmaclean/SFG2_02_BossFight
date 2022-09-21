using Game.Audio;
using UnityEngine;

namespace Game.Core
{
    public class MonoExtended : MonoBehaviour
    {
        public void PlaySFX(SOAudioData data)
        {
            data.Play();
        }
    }
}