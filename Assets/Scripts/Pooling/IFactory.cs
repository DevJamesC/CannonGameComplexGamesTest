using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public interface IFactory<T>
    {
        T Create();
    }

}
