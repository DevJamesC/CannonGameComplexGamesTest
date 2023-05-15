using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Acts as a parent class for weapons to inherit. It could work as an interface as well, but hey, tomayto-tomahto (for this use case, anyway)
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected CannonballController cannonballPrefab;
        [SerializeField] protected Transform launchPoint;
       
        /// <summary>
        /// Use the weapon
        /// </summary>
        public virtual void Use()
        {

        }

        /// <summary>
        /// Stop using the weapon
        /// </summary>
        public virtual void StopUse()
        {

        }
       
    }
}
