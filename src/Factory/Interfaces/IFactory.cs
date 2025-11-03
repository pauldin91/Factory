

namespace Factory.Interfaces
{
    
    public interface IFactory<TIfc> 
    {
        IReadOnlyDictionary<Type, TIfc> Cache { get; }
        IReadOnlyDictionary<string, TIfc> GeneralCache { get; }

        TIfc GetOrAddInstance<T>()
            where T : TIfc, new();
        
        TIfc GetOrAddInstance(Type key);
        TIfc GetOrAddInstance<TImpl>(string typeName);
    }
}