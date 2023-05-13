namespace IWantToWorkAtComplexGames
{
    public class DestroyOnDamage : ResettableMonoBehaviour, IDamageable
    {
        public void TakeDamage(float damage)
        {
            Destroy(gameObject);
        }
    }
}
