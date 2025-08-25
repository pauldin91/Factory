using Factory.Interfaces;
using System.Collections;
using System.Collections.Concurrent;

namespace Factory
{
    public class FactoryWrapperImpl<TIfc> : IFactory<TIfc>
    {
        private readonly ConcurrentDictionary<Type, TIfc> _cache = [];
        private readonly ConcurrentDictionary<string, TIfc> _generalCache = [];
        
        
        public TIfc GetOrAddInstance<T>()
        where T : TIfc,new()
        {
            return _cache.GetOrAdd(typeof(T), (t) => new T() ?? throw new ArgumentException(typeof(T).Name));
        }
        public TIfc GetOrAddInstance(Type type)
        {
            return _cache.GetOrAdd(type, (t) => (TIfc)Activator.CreateInstance(type) ?? throw new ArgumentException(type.Name));
        }
        
        public TIfc GetOrAddInstance<TImpl>(string typeName)
        {
            
            return _generalCache.GetOrAdd(typeName, (t) =>
                {
                    var type = typeof(TImpl)
                        .Assembly
                        .GetTypes()
                        .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(TIfc).IsAssignableFrom(t))
                        .FirstOrDefault(s=>s.Name.Equals(typeName,StringComparison.CurrentCultureIgnoreCase) || 
                                           s.Name.EndsWith(typeName,StringComparison.CurrentCultureIgnoreCase)) ?? throw new ArgumentException("type not found");
                    return GetOrAddInstance(type);
                }
            );
        }
        

        IEnumerator<TIfc> IEnumerable<TIfc>.GetEnumerator()
        {
            return _cache.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cache.Values.GetEnumerator();
        }
        
    }
}