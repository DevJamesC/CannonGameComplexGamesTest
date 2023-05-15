using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class releases an object back to its pool on damage
    /// </summary>
    public class ReturnToPoolOnDamage : Health, IDamageable
    {
        //Invokes on death method when hit. All logic is handled by other scripts via delegation.
        public override void TakeDamage(float damage)
        {
            AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 20);
            InvokeOnDeath();
        }
    }
}
