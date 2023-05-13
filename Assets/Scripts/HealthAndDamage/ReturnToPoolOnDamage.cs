using IWantToWorkAtComplexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolOnDamage : ResettableMonoBehaviour, IDamageable
{
    public event Action<ReturnToPoolOnDamage> OnDamage = delegate { };

    [SerializeField] private AudioClip destroyAudioClip;
    public void TakeDamage(float damage)
    {
        AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 20);
        OnDamage.Invoke(this);
    }
}
