using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Rail Weapon has a charge and release fire control. 
    /// </summary>
    public class RailWeapon : Weapon
    {
        public float CurrentForcePercent
        {
            get
            {
                if (!weaponInUse)
                    return 0;

                return (launchForce - minLaunchForce) / (maxLaunchForce - minLaunchForce);
            }
        }

        [SerializeField] private float maxLaunchForce = 200;
        [SerializeField] private float minLaunchForce = 100;
        [SerializeField] private float timeToMaxForce = 2f;
        [SerializeField] private GameObject launchParticleSystem;
        [SerializeField] private AudioClip launchAudioClip;
        [SerializeField] private AudioSource chargingAudioSource;
        [SerializeField] private AudioSource fullChargeLoopSource;
        [SerializeField] private Slider powerSlider;

        private Pool<CannonballController> cannonballPool;
        private CinemachineImpulseSource impulseSource;
        private float launchForce;
        private bool weaponInUse;

        private void Start()
        {
            cannonballPool = LazyPoolerUtility.GetSimplePool<CannonballController>(cannonballPrefab.gameObject);
            impulseSource = GetComponent<CinemachineImpulseSource>();
            weaponInUse = false;
        }

        private void Update()
        {
            IncrimentLaunchForce();
        }

        /// <summary>
        /// Incriments the launch force, up to the maximum force, depending on how long the weapon is used
        /// </summary>
        private void IncrimentLaunchForce()
        {
            if (!weaponInUse)
                return;

            if (CurrentForcePercent <= 1)
            {
                launchForce += (maxLaunchForce / timeToMaxForce) * Time.deltaTime;
                powerSlider.value = CurrentForcePercent;
            }

            if (CurrentForcePercent >= 1 && !fullChargeLoopSource.isPlaying)
                fullChargeLoopSource.Play();
        }

        /// <summary>
        /// Start charging the weapon
        /// </summary>
        public override void Use()
        {
            weaponInUse = true;
            launchForce = minLaunchForce;
            chargingAudioSource.Play();
        }

        /// <summary>
        /// Release the charge, instanciate the cannonball and any FX.
        /// </summary>
        public override void StopUse()
        {
            CannonballController newCannonball = LazyPoolerUtility.GetSimplePooledObject<CannonballController>(cannonballPrefab.gameObject);
            newCannonball.transform.position = launchPoint.position;
            newCannonball.gameObject.SetActive(true);
            newCannonball.OnCollision += NewCannonball_OnCollision;
            newCannonball.CustomData = new RailCannonballCustomData(Mathf.FloorToInt(Mathf.Lerp(0, 2, CurrentForcePercent)));

            newCannonball.Launch(launchPoint.forward * launchForce);
            //Instantiate(launchParticleSystem, launchPoint.position + (launchPoint.forward * .5f), launchPoint.rotation);
            AudioSource.PlayClipAtPoint(launchAudioClip, launchPoint.position, 5);
            chargingAudioSource.Stop();
            fullChargeLoopSource.Stop();
            weaponInUse = false;
            powerSlider.value = 0;
            impulseSource.GenerateImpulse();
        }

        /// <summary>
        /// Handle decrimenting the cannonball "health" per collision. If it runs out of collisions, release it back into the pool
        /// </summary>
        /// <param name="cannonball"></param>
        /// <param name="collision"></param>
        private void NewCannonball_OnCollision(CannonballController cannonball, Collision collision)
        {

            RailCannonballCustomData customData = cannonball.GetCustomData<RailCannonballCustomData>();
            if (!collision.gameObject.name.Contains("Terrain") && customData.CollisionsBeforeDestroy > 0)
            {
                customData.CollisionsBeforeDestroy--;
                cannonball.Launch(-collision.contacts[0].normal * minLaunchForce);
                return;
            }

            cannonball.OnCollision -= NewCannonball_OnCollision;
            cannonballPool.Release(cannonball);
        }


        /// <summary>
        /// Custom data for the rail cannonball
        /// </summary>
        private class RailCannonballCustomData
        {
            public int CollisionsBeforeDestroy;

            public RailCannonballCustomData()
            {
                CollisionsBeforeDestroy = 0;
            }
            public RailCannonballCustomData(int collisionsBeforeDestroy)
            {
                this.CollisionsBeforeDestroy = collisionsBeforeDestroy;
            }
        }
    }
}
