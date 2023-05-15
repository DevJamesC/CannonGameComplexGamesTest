using IWantToWorkAtComplexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ResettableMonoBehaviour, IDamageable
{
    public event Action<IDamageable> OnDamage = delegate { };
    public event Action<IDamageable> OnDeath=delegate { };

    [SerializeField] protected AudioClip destroyAudioClip;
    [SerializeField] private int maxhealth;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxhealth;
    }
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

    protected void InvokeOnDeath()
    {
        OnDeath.Invoke(this);
    }
    protected void InvokeOnDamage()
    {
        OnDamage.Invoke(this);
    }
}
