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
        private readonly IList<Type> _types = new List<Type> { typeof(ConcreteImplA), typeof(ConcreteImplB), typeof(ConcreteImplC), typeof(ConcreteImplD), typeof(ConcreteImplE) };

        public DependencyInjectionTests()
        {
            sp = new ServiceCollection()
            .AddFactoryFromAssembly(Assembly.GetExecutingAssembly())
            .BuildServiceProvider();

            var factory = sp.GetRequiredService<IFactory>();

            foreach (var type in _types)
            {
                _ = factory.GetInstance(type);
            }
        }

        public void Dispose()
        {
        }

        [Test]
        public void TestRegistrationWithAssembly()
        {
            var factory = sp.GetRequiredService<IFactory>();
            var instanceA = factory.GetInstance(typeof(ConcreteImplA));
            foreach (var type in _types)
            {
                var concrete = factory.GetInstance(type);
                Assert.That(type, Is.EqualTo(concrete.GetType()));
            }
        
        }

        [Test]
        public void TestThatFactoryCachesAllImplementators()
        {
            var factory = sp.GetRequiredService<IFactory>();
            var types = new HashSet<Type> { typeof(ConcreteImplA), typeof(ConcreteImplB), typeof(ConcreteImplC), typeof(ConcreteImplD), typeof(ConcreteImplE) };
            foreach (var instance in factory)
            {
                var exists = types.Any(s => s == instance.GetType());
                Assert.IsTrue(exists);
            }
        }
    }
}