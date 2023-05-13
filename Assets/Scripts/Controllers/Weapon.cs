using Cinemachine;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private CannonballController cannonballPrefab;
        [SerializeField] private Transform launchPoint;
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

        public void Use()
        {
            CannonballController newCannonball = LazyPoolerUtility.GetSimplePooledObject<CannonballController>(cannonballPrefab.gameObject);
            newCannonball.transform.position = launchPoint.position;
            newCannonball.gameObject.SetActive(true);
            newCannonball.OnCollision += NewCannonball_OnCollision;

            newCannonball.Launch(launchPoint.forward * launchForce);
            Instantiate(launchParticleSystem, launchPoint.position+(launchPoint.forward*.5f), launchPoint.rotation);
            AudioSource.PlayClipAtPoint(launchAudioClip, launchPoint.position, 5);

            impulseSource.GenerateImpulse();
        }

        private void NewCannonball_OnCollision(CannonballController cannonball, Collision collision)
        {
            cannonball.OnCollision -= NewCannonball_OnCollision;
            cannonballPool.Release(cannonball);
        }
    }
}
