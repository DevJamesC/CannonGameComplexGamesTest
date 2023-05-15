using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IWantToWorkAtComplexGames
{
    public class AutoWeapon : Weapon
    {
        [SerializeField] private List<Transform> launchPoints;
        [SerializeField] private float launchForce = 100;
        [SerializeField] private GameObject launchParticleSystem;
        [SerializeField] private AudioClip launchAudioClip;
        [SerializeField] private AudioClip reloadingAudioClip;
        [SerializeField] private float refireRate;
        [SerializeField] private int maxClip;
        [SerializeField] private float timeToReload;
        [SerializeField] private Slider ammoCounter;
        

        private Pool<CannonballController> cannonballPool;
        private CinemachineImpulseSource impulseSource;
        private bool weaponInUse;
        private float timeToNextFire;
        private int currentClip;
        private float timeToFinishReload;
        private int currentLaunchPointIndex;

        private void Start()
        {
            cannonballPool = LazyPoolerUtility.GetSimplePool<CannonballController>(cannonballPrefab.gameObject);
            impulseSource = GetComponent<CinemachineImpulseSource>();
            currentClip = maxClip;
            ammoCounter.value = (float)currentClip / maxClip;
            currentLaunchPointIndex = 0;
        }

        private void Update()
        {
            if (timeToFinishReload > 0)
            {
                timeToFinishReload -= Time.deltaTime;

                if (timeToFinishReload <= 0)
                {
                    currentClip = maxClip;
                    ammoCounter.value = (float)currentClip / maxClip;
                }
                return;
            }

            if (timeToNextFire > 0)
                timeToNextFire -= Time.deltaTime;
            else if (weaponInUse)
            {
                timeToNextFire = refireRate;
                Fire();
            }

        }

        public override void Use()
        {
            weaponInUse = true;
        }

        public override void StopUse()
        {
            weaponInUse = false;
        }

        private void Fire()
        {
            launchPoint = launchPoints[currentLaunchPointIndex];
            currentLaunchPointIndex = (currentLaunchPointIndex + 1) % 4;

            CannonballController newCannonball = LazyPoolerUtility.GetSimplePooledObject<CannonballController>(cannonballPrefab.gameObject);
            newCannonball.transform.position = launchPoint.position;
            newCannonball.gameObject.SetActive(true);
            newCannonball.OnCollision += NewCannonball_OnCollision;

            newCannonball.Launch(launchPoint.forward * launchForce);
            Instantiate(launchParticleSystem, launchPoint.position + (launchPoint.forward * .5f), launchPoint.rotation);
            AudioSource.PlayClipAtPoint(launchAudioClip, launchPoint.position, 5);
            impulseSource.GenerateImpulse();

            currentClip--;
            if (currentClip <= 0 && timeToFinishReload <= 0)
            {
                timeToFinishReload = timeToReload;
                AudioSource.PlayClipAtPoint(reloadingAudioClip, gameObject.transform.position);
            }

            ammoCounter.value = (float)currentClip / maxClip;
        }

        private void NewCannonball_OnCollision(CannonballController cannonball, Collision collision)
        {
            cannonball.OnCollision -= NewCannonball_OnCollision;
            cannonballPool.Release(cannonball);
        }
    }
}
