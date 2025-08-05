using Factory.Interfaces;
using Factory.Tests.Implementors;
using FluentAssertions;
using Moq;
using System.ComponentModel;

namespace Factory.Tests
{
    public class FactoryTests
    {

        private readonly IFactory _factory;

        public FactoryTests()
        {
            _factory = new FactoryWrapperImpl();
        }

        [Test]
        public void TestThatFactoryReturnsImplementor()
        {
            var typeA = typeof(ConcreteImplA);
            var typeC = typeof(ConcreteImplC);
            var typeE = typeof(ConcreteImplE);

            Assert.That(typeA.IsEquivalentTo(_factory.GetInstance(typeA).GetType()));
            Assert.That(typeC.IsEquivalentTo(_factory.GetInstance(typeC).GetType()));
            Assert.That(typeE.IsEquivalentTo(_factory.GetInstance(typeE).GetType()));
        }

        [Test]
        public void TestThatFactoryShouldThrowInvalidCastExceptionForNonImplewmentingClasses()
        {
            var typeNot = typeof(NotImplementor);

           Assert.Throws<InvalidCastException>(() => _factory.GetInstance(typeNot).GetType());
        }
    }
}