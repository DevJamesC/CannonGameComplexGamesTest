namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Base factory class. Used to instanciate new objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Factory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }

}
