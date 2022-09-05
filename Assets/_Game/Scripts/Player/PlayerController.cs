using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : SingletonExtended<PlayerController>
    {
        [SerializeField]
        [Min(0)]
        float _speed = 40;

        [SerializeField]
        Smooth2DVector _moveInput;
        Rigidbody _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            Move();
            _moveInput.Update();
        }

        void Move()
        {
            var t = transform;
            Vector2 move  = Time.deltaTime * _speed * _moveInput.Value;
            Vector3 delta = new Vector3(move.x, 0, move.y);
            
            _rb.MovePosition(t.position + delta);
        }
        
        #region Input System
        public void OnMove(InputAction.CallbackContext context) => _moveInput.Target = context.ReadValue<Vector2>();

        public void OnQuit()
        {
            Application.Quit();
            print("Quit Game");
        }
        #endregion
    }
}
