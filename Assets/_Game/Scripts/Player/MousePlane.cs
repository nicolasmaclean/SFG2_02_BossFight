using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class MousePlane
    {
        Vector3 _position;
        public Vector3 Position
        {
            get
            {
                Update();
                return _position;
            }
        }

        Camera _cam;
        public Camera Cam
        {
            get
            {
                if (_cam == null)
                {
                    _cam = Camera.main;
                }

                return _cam;
            }
        }

        static Plane s_groundPlane = new(Vector3.up, 0);
        float _distance;
        
        void Update()
        {
            Ray ray = Cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (s_groundPlane.Raycast(ray, out _distance))
            {
                _position = ray.GetPoint(_distance);
            }
        }
    }
}