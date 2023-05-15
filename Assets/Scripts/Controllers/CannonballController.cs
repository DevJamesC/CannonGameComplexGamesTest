using Cinemachine;
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
        public object CustomData { set => customData = value; }

        [SerializeField] private float lifetime;
        [SerializeField] private GameObject onCollisionParticleSystem;
        [SerializeField] private AudioClip collisionAudioClip;

        public event Action<CannonballController, Collision> OnCollision = delegate { };

        private new Rigidbody rigidbody;
        private float currentLifetime;
        private CinemachineImpulseSource impulseSource;

        private object customData;

        public T GetCustomData<T>() where T : class
        {
            return customData as T;
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void Start()
        {
            currentLifetime = lifetime;
        }

        public void Launch(Vector3 velocity)
        {
            rigidbody.AddForce(velocity, ForceMode.Impulse);
        }

        private void Update()
        {
            if (currentLifetime > 0)
                currentLifetime -= Time.deltaTime;
            else
                Reset();
        }

        public override void Reset()
        {
            base.Reset();
            OnCollision = delegate { };
            rigidbody.velocity = Vector3.zero;
            currentLifetime = lifetime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable target = collision.collider.gameObject.GetComponentInParent<IDamageable>();
            if (target != null)
                DealDamage(target);

            OnCollision.Invoke(this, collision);
            Instantiate(onCollisionParticleSystem, collision.GetContact(0).point, Quaternion.identity);
            AudioSource.PlayClipAtPoint(collisionAudioClip, collision.GetContact(0).point, 50);
            impulseSource.GenerateImpulse(new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f), 0f));

        }

        public void DealDamage(IDamageable target)
        {
            target.TakeDamage(1);
        }
    }
}
