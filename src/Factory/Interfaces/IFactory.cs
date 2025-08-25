
namespace Factory.Interfaces
{
    
    public interface IFactory<TIfc> : IEnumerable<TIfc>
    {
        TIfc GetOrAddInstance<T>()
            where T : TIfc, new();
        
        TIfc GetOrAddInstance(Type key);
        TIfc GetOrAddInstance<TImpl>(string typeName);
    }
}