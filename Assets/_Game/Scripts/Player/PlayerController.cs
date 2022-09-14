using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : SingletonExtended<PlayerController>
    {
        [Header("Stats")]
        [SerializeField]
        [Min(0)]
        float _speed = 10;

        [SerializeField]
        Smooth2DVector _moveInput;
        
        Rigidbody _rb;
        MousePlane _mouse = new();

        protected override void OnAwake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            // apply translation
            Move();
            _moveInput.Update();
            
            // apply rotation
            Look();
        }

        void Move()
        {
            Vector3 move3D = new Vector3(_moveInput.Value.x, 0, _moveInput.Value.y); 
            _rb.velocity = _speed * move3D;
        }

        void Look()
        {
            transform.LookAt(_mouse.Position);
        }

        #region Input System
        public void OnMove(InputAction.CallbackContext context) => _moveInput.Target = context.ReadValue<Vector2>();
        #endregion
    }
}
