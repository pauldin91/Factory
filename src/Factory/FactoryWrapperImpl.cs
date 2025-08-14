using Factory.Interfaces;
using System.Collections;

namespace Factory
{
    public class FactoryWrapperImpl<TIfc> : IFactory<TIfc>
    {
        private readonly Dictionary<Type, TIfc> _cache = [];
        
        public TIfc GetOrAddInstance(Type type)
        {
            if (_cache.TryGetValue(type, out var impl))
            {
                return impl;
            }
            var instance = (TIfc)Activator.CreateInstance(type);
            _cache.Add(type, instance);
            return instance;
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