using System;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Handles receiving damage and decrimenting health
    /// </summary>
    public class Health : ResettableMonoBehaviour, IDamageable
    {
        public event Action<IDamageable> OnDamage = delegate { };
        public event Action<IDamageable> OnDeath = delegate { };

        [SerializeField] protected AudioClip destroyAudioClip;
        [SerializeField] private int maxhealth;

        private int currentHealth;

        private void Start()
        {
            currentHealth = maxhealth;
        }

        /// <summary>
        /// Decriment health and invoke OnDamage and OnDeath events when health is 0
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(float damage)
        {
            currentHealth--;
            InvokeOnDamage();

            if (currentHealth > 0)
                return;

            AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 20);
            InvokeOnDeath();
        }

        public override void Reset()
        {
            base.Reset();
            currentHealth = maxhealth;
        }

        /// <summary>
        /// Wrapper method so child classes can invoke onDamage
        /// </summary>
        protected void InvokeOnDamage()
        {
            OnDamage.Invoke(this);
        }

        /// <summary>
        /// Wrapper method so child classes can invoke onDeath
        /// </summary>
        protected void InvokeOnDeath()
        {
            OnDeath.Invoke(this);
        }

    }
}
