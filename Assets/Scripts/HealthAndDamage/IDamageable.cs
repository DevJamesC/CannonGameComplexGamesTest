using System;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public interface IDamageable
    {
        GameObject gameObject { get; }
        event Action<IDamageable> OnDamage;
        event Action<IDamageable> OnDeath;
        void TakeDamage(float damage);
    }
}
