using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    public class LazyPoolerUtility
    {
        private static Dictionary<GameObject, object> activePools;

        public static T GetSimplePooledObject<T>(GameObject prefab) where T : ResettableMonoBehaviour
        {
            if (activePools == null)
                activePools = new Dictionary<GameObject, object>();

            Pool<T> pool = GetPool<T>(prefab);
            pool.CleanPool();

            return pool.Allocate();
        }

        public static Pool<T> GetSimplePool<T>(GameObject prefab) where T : ResettableMonoBehaviour
        {
            if (activePools == null)
                activePools = new Dictionary<GameObject, object>();

            Pool<T> pool = GetPool<T>(prefab);
            pool.CleanPool();

            return pool;
        }

        private static Pool<T> GetPool<T>(GameObject prefab) where T : ResettableMonoBehaviour
        {
            //check if we have a pool for this game object. If not, create the pool.
            object pool;
            if (!activePools.TryGetValue(prefab, out pool))
            {
                Pool<T> newPool = new Pool<T>(new PrefabFactory<T>(prefab), 1);
                pool = newPool;
                activePools.Add(prefab, newPool);
            }

            return pool as Pool<T>;
        }

    }
}
