using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private CannonballController cannonballPrefab;
        [SerializeField] private Transform launchPoint;
        [SerializeField] private float launchForce = 100;

        private Pool<CannonballController> cannonballPool;

        private void Start()
        {
            cannonballPool = LazyPoolerUtility.GetSimplePool<CannonballController>(cannonballPrefab.gameObject);
        }

        public void Use()
        {
            CannonballController newCannonball = LazyPoolerUtility.GetSimplePooledObject<CannonballController>(cannonballPrefab.gameObject);
            newCannonball.transform.position = launchPoint.position;
            newCannonball.gameObject.SetActive(true);
            newCannonball.OnCollision += NewCannonball_OnCollision;

            newCannonball.Launch(launchPoint.forward * launchForce);
        }

        private void NewCannonball_OnCollision(CannonballController cannonball, Collision collision)
        {
            cannonballPool.Release(cannonball);
        }
    }
}
