using System;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Interface for classes to handle objects taking damage
    /// </summary>
    public interface IDamageable
    {
        GameObject gameObject { get; }
        event Action<IDamageable> OnDamage;
        event Action<IDamageable> OnDeath;
        void TakeDamage(float damage);
    }
}
