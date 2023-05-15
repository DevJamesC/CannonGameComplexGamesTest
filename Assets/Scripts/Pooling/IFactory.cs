
namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Factory Interface for a class which will be used to instanciate new objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<T>
    {
        T Create();
    }

}
