using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Audio
{
    [CreateAssetMenu(menuName = "Data/Audio Data")]
    public class SOAudioData : ScriptableObject
    {
        public static Transform s_parent
        {
            get
            {
                if (_parent == null)
                {
                    _parent = new GameObject("GRP_Audio").transform;
                }
                return _parent;
            }
        }
        static Transform _parent;
        
        public AudioClip Clip;
        
        [Range(0, 1)]
        public float volume = 0.5f;

        public void PlayStupid() => Play();
        public GameObject Play()
        {
            // create audio source
            GameObject go = new GameObject(Clip.name);
            AudioSource source = go.AddComponent<AudioSource>();
            go.transform.parent = s_parent;

            // play clip
            source.Load(this);
            source.Play();
            
            // destroy clip when done
            Destroy(go, Clip.length);
            return go;
        }
        
        public void Play(Vector3 position)
        {
            GameObject go = Play();
            go.transform.position = position;
        }
    }
    
    public static class AudioSourceExtensions
    {
        public static void Load(this AudioSource source, SOAudioData data)
        {
            source.clip = data.Clip;
            source.volume = data.volume;
        }
    }
}