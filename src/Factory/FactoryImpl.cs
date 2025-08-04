using Factory.Interfaces;
using System.Reflection;

namespace Factory
{
    public class FactoryImpl : IFactory
    {
        private readonly Dictionary<Type, IImplementor> cache = [];
        public IReadOnlyDictionary<Type, IImplementor> Cache => cache;

        public IImplementor GetInstance(Type type)
        {
            if (cache.TryGetValue(type, out var impl))
            {
                return impl;
            }
            var instance = (IImplementor)Activator.CreateInstance(type);
            cache.Add(type, instance);
            return instance;
        }


        
    }
}