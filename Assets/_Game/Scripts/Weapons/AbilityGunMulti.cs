using UnityEngine;

namespace Game._Game.Scripts.Weapons
{
    public class AbilityGunMulti : AbilityGun
    {
        [SerializeField]
        Transform[] _additionalGuns;
        
        public override bool Fire()
        {
            bool success = base.Fire();

            if (success)
            {
                foreach (var gun in _additionalGuns)
                {
                    SpawnBullet(_bulletPrefab, gun, _damage, gameObject.layer);
                }
            }
            
            return success;
        }
    }
}