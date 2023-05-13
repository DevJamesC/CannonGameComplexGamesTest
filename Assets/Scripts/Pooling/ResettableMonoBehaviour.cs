using IWantToWorkAtComplexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class ResettableMonoBehaviour : MonoBehaviour, IResettable
    {
        public virtual void Reset()
        {
            gameObject.SetActive(false);
        }
    }
}
