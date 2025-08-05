using Factory.Interfaces;
using System.Collections;

namespace Factory
{
    public class FactoryWrapperImpl : IFactory
    {
        private readonly Dictionary<Type, IImplementor> cache = [];

        public IEnumerator<IImplementor> GetEnumerator()
        {
            throw new NotImplementedException();
        }

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

        IEnumerator<IImplementor> IEnumerable<IImplementor>.GetEnumerator()
        {
            return cache.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cache.Values.GetEnumerator();
        }


    }
}