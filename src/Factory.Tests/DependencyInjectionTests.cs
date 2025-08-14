using Factory.DependencyInjection;
using Factory.Interfaces;
using Factory.Tests.Implementors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Factory.Tests
{
    public class DependencyInjectionTests : IDisposable
    {
        private readonly IServiceProvider sp;
        private readonly HashSet<Type> _types = new () { typeof(ConcreteImplA), typeof(ConcreteImplB), typeof(ConcreteImplC), typeof(ConcreteImplD), typeof(ConcreteImplE) };

        public DependencyInjectionTests()
        {
            sp = new ServiceCollection()
            .AddFactory<IImplementor,ConcreteImplA>()
            .BuildServiceProvider();

            var factory = sp.GetRequiredService<IFactory<IImplementor>>();

            foreach (var type in _types)
            {
                _ = factory.GetOrAddInstance(type);
            }
        }

        public void Dispose() { }
        
        [Test]
        public void TestRegistrationWithAssembly()
        {
            var factory = sp.GetRequiredService<IFactory<IImplementor>>();
            var instanceA = factory.GetOrAddInstance(typeof(ConcreteImplA));
            foreach (var type in _types)
            {
                var concrete = factory.GetOrAddInstance(type);
                Assert.That(type, Is.EqualTo(concrete.GetType()));
            }
        }

        [Test]
        public void TestThatFactoryCachesAllImplementators()
        {
            var factory = sp.GetRequiredService<IFactory<IImplementor>>();
            Assert.That(factory.All(s=> _types.Contains(s.GetType())), Is.True);
        }
    }
}