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

        public void Spawn(TransformData data)
        {
            StoppableParticles particles = Instantiate(this, data.Position, data.Rotation);
            particles.Play();
            Destroy(particles.gameObject, particles.GetComponent<ParticleSystem>().main.duration);
        }
    }
    
    public struct TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public TransformData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}