using Cinemachine;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Cannon weapon. Simple fire control as fast as keys can be pressed
    /// </summary>
    public class CannonWeapon : Weapon
    {
        [SerializeField] private float launchForce = 100;
        [SerializeField] private GameObject launchParticleSystem;
        [SerializeField] private AudioClip launchAudioClip;

        private Pool<CannonballController> cannonballPool;
        private CinemachineImpulseSource impulseSource;

        private void Start()
        {
            cannonballPool = LazyPoolerUtility.GetSimplePool<CannonballController>(cannonballPrefab.gameObject);
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        /// <summary>
        /// Instanciates and launches the cannonball
        /// </summary>
        public override void Use()
        {
            CannonballController newCannonball = LazyPoolerUtility.GetSimplePooledObject<CannonballController>(cannonballPrefab.gameObject);
            newCannonball.transform.position = launchPoint.position;
            newCannonball.gameObject.SetActive(true);
            newCannonball.OnCollision += NewCannonball_OnCollision;           

            newCannonball.Launch(launchPoint.forward * launchForce);
            Instantiate(launchParticleSystem, launchPoint.position + (launchPoint.forward * .5f), launchPoint.rotation);
            AudioSource.PlayClipAtPoint(launchAudioClip, launchPoint.position, 5);

            impulseSource.GenerateImpulse();
        }

        /// <summary>
        /// Releases the cannonball on collision
        /// </summary>
        /// <param name="cannonball"></param>
        /// <param name="collision"></param>
        private void NewCannonball_OnCollision(CannonballController cannonball, Collision collision)
        {
            cannonball.OnCollision -= NewCannonball_OnCollision;
            cannonballPool.Release(cannonball);
        }

        
    }
}
