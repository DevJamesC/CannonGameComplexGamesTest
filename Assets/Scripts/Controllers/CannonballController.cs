using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class handles the cannonball projectile logic, such as collisions and damage
    /// </summary>
    public class CannonballController : ResettableMonoBehaviour, IDamager
    {
        public event Action<CannonballController, Collision> OnCollision = delegate { };

        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 velocity)
        {
            rigidbody.AddForce(velocity, ForceMode.Impulse);
        }

        public override void Reset()
        {
            base.Reset();
            OnCollision = delegate { };
            rigidbody.velocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable target = collision.collider.gameObject.GetComponentInParent<IDamageable>();
            if (target != null)
                DealDamage(target);

            OnCollision.Invoke(this, collision);

           
        }

        public void DealDamage(IDamageable target)
        {
            target.TakeDamage(1);
        }
    }
}
