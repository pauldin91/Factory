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


        public DependencyInjectionTests()
        {
            sp = new ServiceCollection()
            .AddFactoryFromAssembly(Assembly.GetExecutingAssembly())
            .BuildServiceProvider();
        }

        public void Dispose()
        {
        }

        [Test]
        public void TestRegistrationWithAssembly()
        {
            var factory = sp.GetRequiredService<IFactory>();
            var instanceA = factory.GetInstance(typeof(ConcreteImplA));
            var types = new List<Type> { typeof(ConcreteImplA), typeof(ConcreteImplB), typeof(ConcreteImplC), typeof(ConcreteImplD), typeof(ConcreteImplE) };
            foreach (var type in types)
            {
                var concrete = factory.GetInstance(type);
                Assert.That(type,Is.EqualTo(concrete.GetType()));
            }
        }

        [Test]
        public void TestThatFactoryCachesAllImplementators()
        {
            var factory = sp.GetRequiredService<IFactory>();
            Assert.That(factory.Cache.Count, Is.EqualTo(5));

        }
    }
}