
namespace Factory.Interfaces
{
    
    public interface IFactory<out TIfc> : IEnumerable<TIfc>
    {
        TIfc GetOrAddInstance(Type key);
    }
}