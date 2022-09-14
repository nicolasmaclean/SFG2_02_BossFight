using System;
using UnityEngine;

namespace Game.Utils
{
    [RequireComponent(typeof(ParticleSystem))]
    public class StoppableParticles : MonoBehaviour, IStoppable
    {
        ParticleSystem _particle;

        void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        public void Start()
        {
            _particle.Play();
        }

        public void Stop()
        {
            _particle.Stop();
        }
    }
}