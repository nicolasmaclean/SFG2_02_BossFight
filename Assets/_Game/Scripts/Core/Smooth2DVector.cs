using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public class Smooth2DVector
    {
        [ReadOnly]
        public Vector2 Target = Vector2.zero;
        
        Vector2 _value;
        public Vector2 Value => _value;

        public float Gravity = 4;

        public void FixedUpdate() => Update(Time.fixedDeltaTime); 
        public void Update() => Update(Time.deltaTime);
        
        void Update(float timeStep)
        {
            _value.x = Mathf.MoveTowards(_value.x, Target.x, timeStep * Gravity);
            _value.y = Mathf.MoveTowards(_value.y, Target.y, timeStep * Gravity);
        }
    }
}