using System.Collections;
using Game.Core;
using Game.Player;
using Game.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class KillableKnockback : Killable
    {
        [SerializeField]
        float _mult = 8;
        
        Rigidbody _rb;
        PlayerController _controller;

        protected override void Awake()
        {
            base.Awake();
            
            _rb = GetComponent<Rigidbody>();
            _controller = GetComponent<PlayerController>();
        }

        public void HurtWithKnockback(float damage, Vector3 from)
        {
            // apply knockback if not dead
            if (Hurt(damage))
            {
                // turn off player controller
                _controller.enabled = false;
                
                // calculate force
                Vector3 dir = (transform.position - from).normalized;
                dir *= _mult;
                
                // apply force
                _rb.AddForce(dir, ForceMode.Impulse);

                StartCoroutine(Coroutines.WaitThen(.05f, () =>
                {
                    StartCoroutine(Coroutines.WaitTill(
                        () => _rb.velocity.sqrMagnitude < .1f,
                        () => _controller.enabled = true
                    ));
                }));
            }
        }
    }
}