using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Player;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knockbackable : MonoBehaviour
    {
        [SerializeField]
        float _mult = 8;
        
        Rigidbody _rb;
        PlayerController _controller;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _controller = GetComponent<PlayerController>();
        }

        public void Apply(Vector3 from)
        {
            // turn off player controller, temporarily
            if (_controller)
            {
                _controller.enabled = false;
                StartCoroutine(Coroutines.WaitThen(.05f, () =>
                {
                    StartCoroutine(Coroutines.WaitTill(
                        () => _rb.velocity.sqrMagnitude < .1f,
                        () => _controller.enabled = true
                    ));
                }));
            }
            
            // calculate force
            Vector3 dir = (transform.position - from).normalized;
            dir *= _mult;
            
            // apply force
            _rb.AddForce(dir, ForceMode.Impulse);
        }
    }
}
