using IWantToWorkAtComplexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolOnDamage : Health, IDamageable
{
    public override void TakeDamage(float damage)
    {
        AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position, 20);
        InvokeOnDeath();
    }
}
