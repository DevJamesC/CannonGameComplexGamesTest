using Cinemachine;
using System;
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
        private float lifetimeRemaining;
        private CinemachineImpulseSource impulseSource;
        private TrailRenderer trailRenderer;

        private object customData;

        /// <summary>
        /// Returns custom data, if custom data has been set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCustomData<T>() where T : class
        {
            if (customData == null)
                return null;

            return customData as T;
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
            trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Start()
        {
            lifetimeRemaining = lifetime;
            trailRenderer.Clear();
        }

        /// <summary>
        /// Adds force to the cannonball
        /// </summary>
        /// <param name="velocity"></param>
        public void Launch(Vector3 velocity)
        {
            rigidbody.AddForce(velocity, ForceMode.Impulse);
        }

        //Handles decrimenting the lifetimeRemaining counter, and releasing the object back into the pool if it travels without a collision for too long
        private void Update()
        {
            if (lifetimeRemaining > 0)
                lifetimeRemaining -= Time.deltaTime;
            else
                Reset();
        }

        public override void Reset()
        {
            base.Reset();
            OnCollision = delegate { };
            rigidbody.velocity = Vector3.zero;
            lifetimeRemaining = lifetime;
        }

        /// <summary>
        /// Handles instanciating VFX and SFX, generating camera shake impulse, and invoking the OnCollisionEvent
        /// </summary>
        /// <param name="collision"></param>
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
        
        //Clears the trailRenderer when enabled, so we don't get huge lines from the cannonball's last position
        private void OnEnable()
        {
            trailRenderer.Clear();
        }

        /// <summary>
        /// Deal 1 damage to a target
        /// </summary>
        /// <param name="target"></param>
        public void DealDamage(IDamageable target)
        {
            target.TakeDamage(1);
        }
    }
}
