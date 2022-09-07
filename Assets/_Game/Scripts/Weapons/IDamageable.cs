using System.Transactions;

namespace Game.Weapons
{
    public interface IDamageable
    {
        public void Hurt(float damage);
    }
}