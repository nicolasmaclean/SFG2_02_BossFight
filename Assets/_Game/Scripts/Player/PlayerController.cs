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
        float _speed = 40;

        [SerializeField]
        [Min(0)]
        float _fireCooldown = .3f;

        [SerializeField]
        Smooth2DVector _moveInput;
        
        [Header("Attack")]
        [SerializeField]
        Transform _originBullet;

        [SerializeField]
        GameObject _bulletPrefab;

        public UnityEvent OnShoot;

        float _fireTimestamp;
        
        Rigidbody _rb;
        MousePlane _mouse = new();

        void Awake()
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
            var t = transform;
            Vector2 move  = Time.deltaTime * _speed * _moveInput.Value;
            Vector3 delta = new Vector3(move.x, 0, move.y);
            
            _rb.MovePosition(t.position + delta);
        }

        void Look()
        {
            transform.LookAt(_mouse.Position);
        }

        void Fire()
        {
            float elapsedTime = Time.time - _fireTimestamp;
            if (elapsedTime < _fireCooldown) return;

            _fireTimestamp = Time.time;
            
            GameObject bullet = Instantiate(_bulletPrefab, _originBullet.position, _originBullet.rotation);
            bullet.layer = gameObject.layer;
            
            OnShoot?.Invoke();
        }

        void Quit()
        {
            Application.Quit();
            print("Quit Game");
        }
        
        #region Input System
        public void OnMove(InputAction.CallbackContext context) => _moveInput.Target = context.ReadValue<Vector2>();

        public void OnFire(InputAction.CallbackContext context)
        {
            // activate on button down
            if (!context.performed) return;
            
            Fire();
        }
        public void OnQuit() => Quit();

        #endregion
    }
}
