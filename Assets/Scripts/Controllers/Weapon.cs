using Cinemachine;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected CannonballController cannonballPrefab;
        [SerializeField] protected Transform launchPoint;
       

        public virtual void Use()
        {

        }

        public virtual void StopUse()
        {

        }
       
    }
}
