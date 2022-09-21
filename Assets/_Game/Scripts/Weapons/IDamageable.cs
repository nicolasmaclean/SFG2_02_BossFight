using System.Transactions;

namespace Game.Weapons
{
    public interface IDamageable
    {
        public bool Hurt(float damage);
    }
}