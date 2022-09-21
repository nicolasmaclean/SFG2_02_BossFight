using System;
using UnityEngine;

namespace Game.Utils
{
    public class ColliderMessager : MonoBehaviour
    {
        public Action<Collision> CollisionEnter;
        public Action<Collision> CollisionStay;
        public Action<Collision> CollisionExit;
        
        public Action<Collider> TriggerEnter;
        public Action<Collider> TriggerStay;
        public Action<Collider> TriggerExit;

        void OnCollisionEnter(Collision collision) => CollisionEnter?.Invoke(collision);
        void OnCollisionStay(Collision collision) => CollisionStay?.Invoke(collision);
        void OnCollisionExit(Collision collision) => CollisionExit?.Invoke(collision);

        void OnTriggerEnter(Collider other) => TriggerEnter?.Invoke(other);
        void OnTriggerStay(Collider other) => TriggerStay?.Invoke(other);
        void OnTriggerExit(Collider other) => TriggerExit?.Invoke(other);
    }
}