using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Base class for a monoBehaviour that can be reset for pooling
    /// </summary>
    public class ResettableMonoBehaviour : MonoBehaviour, IResettable
    {
        public virtual void Reset()
        {
            gameObject.SetActive(false);
        }
    }
}
