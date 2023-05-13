using System;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class DestroyOnDamage : ResettableMonoBehaviour, IDamageable
    {
        [SerializeField] private AudioClip destroyAudioClip;
        public void TakeDamage(float damage)
        {
            AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 10);
            Destroy(gameObject);
        }
    }
}
