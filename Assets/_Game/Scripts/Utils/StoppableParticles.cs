using System;
using System.Collections;
using UnityEngine;

namespace Game.Utils
{
    public class StoppableParticles : MonoBehaviour, IStoppable
    {
        ParticleSystem _particle;

        void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
            if (!_particle)
            {
                _particle = GetComponentInChildren<ParticleSystem>();
            }
        }

        public void Play()
        {
            _particle.Play();
        }

        public void Stop()
        {
            _particle.Stop();
        }
    }
}