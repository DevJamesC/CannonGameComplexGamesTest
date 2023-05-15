using System.Collections;
using System.Collections.Generic;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Provides pooling for members, including creating and allocating out members to other logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> : IEnumerable where T : IResettable
    {

        public List<T> members = new List<T>();
        public HashSet<T> unavailable = new HashSet<T>();
        IFactory<T> factory;

        public Pool(IFactory<T> factory) : this(factory, 5) { }

        public Pool(IFactory<T> factory, int poolSize)
        {
            this.factory = factory;

            for (int i = 0; i < poolSize; i++)
            {
                Create();
            }
        }

        /// <summary>
        /// Returns a pooled object, and will create a new one if none are available
        /// </summary>
        /// <returns></returns>
        public T Allocate()
        {
            for (int i = 0; i < members.Count; i++)
            {
                if (!unavailable.Contains(members[i]))
                {
                    unavailable.Add(members[i]);
                    return members[i];
                }
            }
            T newMember = Create();
            unavailable.Add(newMember);
            return newMember;
        }

        /// <summary>
        /// Reset this member and remove it from the unavailable list
        /// </summary>
        /// <param name="member"></param>
        public void Release(T member)
        {
            member.Reset();
            unavailable.Remove(member);
        }

        /// <summary>
        /// Remove the object from the pool completely
        /// </summary>
        /// <param name="member"></param>
        public void RemoveFromPool(T member)
        {
            unavailable.Remove(member);
            members.Remove(member);
        }

        /// <summary>
        /// Since pools do not exist in a scene, switching scenes will destroy members while keeping the pool. Ideally, memebers would clean
        /// themselves on the onDestroy event, but that solution will need to wait for a refactor.
        /// </summary>
        public void CleanPool()
        {
            List<T> newMembers = new List<T>();
            for (int i = 0; i < members.Count; i++)
            {
                if (!members[i].Equals(null))
                    newMembers.Add(members[i]);
            }
            members = newMembers;

        }

        /// <summary>
        /// Create a new member using a factory
        /// </summary>
        /// <returns></returns>
        T Create()
        {
            T member = factory.Create();
            members.Add(member);
            return member;
        }

        /// <summary>
        /// Required method to allow for enumeration in loops
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)members.GetEnumerator();
        }

    }
}
