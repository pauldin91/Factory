using Factory.Interfaces;
using Factory.Tests.Implementors;

namespace Factory.Tests
{
    public class FactoryTests
    {

        private readonly IFactory<IImplementor> _factory = new FactoryWrapperImpl<IImplementor>();

        [Test]
        public void TestGetInstance_ShouldReturn_ImplementorByType()
        {
            var typeA = typeof(ConcreteImplA);
            var typeC = typeof(ConcreteImplC);
            var typeE = typeof(ConcreteImplE);

            Assert.That(typeA.IsEquivalentTo(_factory.GetOrAddInstance(typeA).GetType()));
            Assert.That(typeC.IsEquivalentTo(_factory.GetOrAddInstance(typeC).GetType()));
            Assert.That(typeE.IsEquivalentTo(_factory.GetOrAddInstance(typeE).GetType()));
        }
        [Test]
        public void TestGetInstance_ShouldReturn_ImplementorByGenericArgument()
        {
            var typeA = typeof(ConcreteImplA);
            var typeC = typeof(ConcreteImplC);
            var typeE = typeof(ConcreteImplE);

            Assert.That(typeA.IsEquivalentTo(_factory.GetOrAddInstance<ConcreteImplA>().GetType()));
            Assert.That(typeC.IsEquivalentTo(_factory.GetOrAddInstance<ConcreteImplC>().GetType()));
            Assert.That(typeE.IsEquivalentTo(_factory.GetOrAddInstance<ConcreteImplE>().GetType()));
        }

        [Test]
        public void TestThatFactory_ShouldThrowInvalidCastException_ForNonImplementingClasses()
        {
            var typeNot = typeof(NotImplementor);

           Assert.Throws<InvalidCastException>(() => _factory.GetOrAddInstance(typeNot).GetType());
        }

        [Test]
        public void TestGetOrAddInstance_ShouldReturn_CorrectImplementationByTypeName()
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

            var types = new Dictionary<string, Tuple<string,Type>>
            {
                {"a",Tuple.Create<string,Type>(nameof(ConcreteImplA),typeof(ConcreteImplA))},
                {"b",Tuple.Create<string,Type>(nameof(ConcreteImplB),typeof(ConcreteImplB))},
                {"c",Tuple.Create<string,Type>(nameof(ConcreteImplC),typeof(ConcreteImplC))},
                {"d",Tuple.Create<string,Type>(nameof(ConcreteImplD),typeof(ConcreteImplD))},
                {"e",Tuple.Create<string,Type>(nameof(ConcreteImplE),typeof(ConcreteImplE))},
            };
            
            foreach (var item in types)
            {
                var instance = _factory.GetOrAddInstance<ConcreteImplA>(item.Value.Item1);
                Assert.That(instance.GetType(), Is.EqualTo(item.Value.Item2));
                Assert.That(instance.GetMsg(), Is.EqualTo($"Inside {item.Value.Item1}"));
            }
        }
    }
}