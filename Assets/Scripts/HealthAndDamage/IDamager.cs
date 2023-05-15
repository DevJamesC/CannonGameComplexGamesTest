namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Interface for classes to handle objects dealing damage
    /// </summary>
    public interface IDamager
    {
        void DealDamage(IDamageable target);
    }
}
