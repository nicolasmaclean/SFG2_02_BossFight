using Game.Weapons;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class BossHalves : MonoBehaviour, IDamageable
    {
        [SerializeField]
        bool _isLeft;
        
        EnemyBoss _parent;

        void Awake()
        {
            _parent = GetComponentInParent<EnemyBoss>();
        }

        public void Hurt(float damage)
        {
            _parent.Hurt(damage, _isLeft);
        }
    }
}