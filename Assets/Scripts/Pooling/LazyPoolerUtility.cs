using System.Collections.Generic;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class provides static methods for logic to receive pooled objects with minimal setup
    /// </summary>
    public class LazyPoolerUtility
    {
        private static Dictionary<GameObject, object> activePools;

        /// <summary>
        /// Get a pooled object from a simple pool. If the pool does not exists, this will create it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static T GetSimplePooledObject<T>(GameObject prefab) where T : ResettableMonoBehaviour
        {
            if (activePools == null)
                activePools = new Dictionary<GameObject, object>();

            Pool<T> pool = GetPool<T>(prefab);
            pool.CleanPool();

            return pool.Allocate();
        }

        /// <summary>
        /// This will return the pool of type T. Having reference to a pool is useful for releasing members.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static Pool<T> GetSimplePool<T>(GameObject prefab) where T : ResettableMonoBehaviour
        {
            if (activePools == null)
                activePools = new Dictionary<GameObject, object>();

            Pool<T> pool = GetPool<T>(prefab);
            pool.CleanPool();

            return pool;
        }

        /// <summary>
        /// Private method to get or create a pool, since the logic is duplicated between methods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab"></param>
        /// <returns></returns>
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
