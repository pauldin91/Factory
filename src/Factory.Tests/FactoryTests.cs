using Factory.Interfaces;
using Factory.Tests.Implementors;
using FluentAssertions;
using Moq;
using System.ComponentModel;

namespace Factory.Tests
{
    public class FactoryTests
    {

        private readonly IFactory<IImplementor> _factory = new FactoryWrapperImpl<IImplementor>();

        [Test]
        public void TestThatFactoryReturnsImplementor()
        {
            var typeA = typeof(ConcreteImplA);
            var typeC = typeof(ConcreteImplC);
            var typeE = typeof(ConcreteImplE);

            Assert.That(typeA.IsEquivalentTo(_factory.GetOrAddInstance(typeA).GetType()));
            Assert.That(typeC.IsEquivalentTo(_factory.GetOrAddInstance(typeC).GetType()));
            Assert.That(typeE.IsEquivalentTo(_factory.GetOrAddInstance(typeE).GetType()));
        }

        [Test]
        public void TestThatFactoryShouldThrowInvalidCastExceptionForNonImplewmentingClasses()
        {
            var typeNot = typeof(NotImplementor);

           Assert.Throws<InvalidCastException>(() => _factory.GetOrAddInstance(typeNot).GetType());
        }

        [Test]
        public void TestThatFactoryShouldReturnInstancesAssemblyScan()
        {
            
            var types = typeof(ConcreteImplA)
                .Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IImplementor)) && !t.IsAbstract && !t.IsInterface)
                .ToDictionary(s=>s.Name, s=>s);
            
            foreach (var item in types)
            {
                var instance = _factory.GetOrAddInstance(item.Value);
                Assert.That(instance.GetType(), Is.EqualTo(item.Value));
                
            }

        }
    }
}