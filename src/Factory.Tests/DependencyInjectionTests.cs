using Factory.DependencyInjection;
using Factory.Interfaces;
using Factory.Tests.Implementors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Factory.Tests
{
    public class DependencyInjectionTests : IDisposable
    {
        private readonly IServiceProvider _sp;
        private readonly HashSet<Type> _types =
        [
            typeof(ConcreteImplA), typeof(ConcreteImplB), typeof(ConcreteImplC), typeof(ConcreteImplD),
            typeof(ConcreteImplE)
        ];

        public DependencyInjectionTests()
        {
            _sp = new ServiceCollection()
            .AddFactory<IImplementor,ConcreteImplA>()
            .BuildServiceProvider();

            var factory = _sp.GetRequiredService<IFactory<IImplementor>>();

            foreach (var type in _types)
            {
                _ = factory.GetOrAddInstance(type);
            }
        }

        public void Dispose() { }
        
        [Test]
        public void TestRegistrationWithAssembly()
        {
            var factory = _sp.GetRequiredService<IFactory<IImplementor>>();
            foreach (var type in _types)
            {
                var concrete = factory.GetOrAddInstance(type);
                Assert.That(type, Is.EqualTo(concrete.GetType()));
            }
        }

        [Test]
        public void TestThatFactoryCachesAllImplementators()
        {
            var factory = _sp.GetRequiredService<IFactory<IImplementor>>();
            Assert.That(factory.All(s=> _types.Contains(s.GetType())), Is.True);
        }
    }
}