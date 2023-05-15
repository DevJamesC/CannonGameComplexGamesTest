using System;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class DestroyOnDamage : ResettableMonoBehaviour, IDamageable
    {
        [SerializeField] private AudioClip destroyAudioClip;

        public event Action<IDamageable> OnDamage = delegate { };
        public event Action<IDamageable> OnDeath = delegate { };

        public void TakeDamage(float damage)
        {
            AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 10);
            OnDamage.Invoke(this);
            Destroy(gameObject);
        }
    }
}
