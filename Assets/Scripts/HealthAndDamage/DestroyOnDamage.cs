using System;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Destroy the object when it takes damage
    /// </summary>
    public class DestroyOnDamage : ResettableMonoBehaviour, IDamageable
    {
        [SerializeField] private AudioClip destroyAudioClip;

        public event Action<IDamageable> OnDamage = delegate { };
        public event Action<IDamageable> OnDeath = delegate { };

        //Play death audio, invoke events, and destroy the object
        public void TakeDamage(float damage)
        {
            AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 10);
            OnDamage.Invoke(this);
            OnDeath.Invoke(this);
            Destroy(gameObject);
        }
    }
}
