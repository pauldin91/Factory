using Factory.Interfaces;
using Factory.Tests.Implementors;

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
        public void TestThatFactoryShouldReturnInstancesByType()
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
                Assert.That(instance.GetMsg(), Is.EqualTo($"Inside {item.Key}"));
                
            }
        }
        
        [Test]
        public void TestThatFactoryShouldReturnInstancesByTypeName()
        {
            
            var types = typeof(ConcreteImplA)
                .Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IImplementor)) && !t.IsAbstract && !t.IsInterface)
                .Select(s=>s.Name)
                .ToList();
            
            foreach (var item in types)
            {
                var instance = _factory.GetOrAddInstance<ConcreteImplA>(item);
                Assert.That(instance.GetType().Name, Is.EqualTo(item));
                Assert.That(instance.GetMsg(), Is.EqualTo($"Inside {item}"));
            }
        }
    }
}