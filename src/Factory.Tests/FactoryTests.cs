using Factory.Interfaces;
using Factory.Tests.Models.Encoders;
using Factory.Tests.Models.NotEncoders;
using Factory.Tests.Options;
using Microsoft.Extensions.Configuration;

namespace Factory.Tests
{
    public class FactoryTests
    {

        private readonly IFactory<IEncoder> _factory = new FactoryWrapperImpl<IEncoder>();
        
        
        
        [Test]
        public void TestGetOrAddInstance_ShouldReturnInstances_ByGenericArgument()
        {
            var typeA = typeof(EncoderFormatA);
            var typeC = typeof(EncoderFormatC);
            var typeE = typeof(EncoderFormatE);

            Assert.That(typeA.IsEquivalentTo(_factory.GetOrAddInstance<EncoderFormatA>().GetType()));
            
            Assert.That(typeC.IsEquivalentTo(_factory.GetOrAddInstance<EncoderFormatC>().GetType()));
            Assert.That(typeE.IsEquivalentTo(_factory.GetOrAddInstance<EncoderFormatE>().GetType()));
        }
        
        [Test]
        public void TestGetOrAddInstance_ShouldReturnInstances_ByType()
        {
            
            var types = typeof(EncoderFormatA)
                .Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEncoder)) && !t.IsAbstract && !t.IsInterface)
                .ToDictionary(s=>s.Name, s=>s);
            
            foreach (var item in types)
            {
                var instance = _factory.GetOrAddInstance(item.Value);
                Assert.That(instance.GetType(), Is.EqualTo(item.Value));
                Assert.That(instance.GetMsg(), Is.EqualTo($"Inside {item.Key}"));
                
            }
        }
        

        [Test]
        public void TestGetOrAddInstance_ShouldThrowInvalidCastException_ForNonImplementingClasses()
        {
            var typeNot = typeof(NotEncoder);

           Assert.Throws<InvalidCastException>(() => _factory.GetOrAddInstance(typeNot).GetType());
        }


        
        [Test]
        public void TestGetOrAddInstance_ShouldReturnInstances_ByTypeInAssembly()
        {

            var types = new Dictionary<string, Tuple<string,Type>>
            {
                {".abc",Tuple.Create(nameof(EncoderFormatA).Split('.').Last(),typeof(EncoderFormatA))},
                {".bcd",Tuple.Create(nameof(EncoderFormatB).Split('.').Last(),typeof(EncoderFormatB))},
                {".cde",Tuple.Create(nameof(EncoderFormatC).Split('.').Last(),typeof(EncoderFormatC))},
                {".def",Tuple.Create(nameof(EncoderFormatD).Split('.').Last(),typeof(EncoderFormatD))},
                {".efg",Tuple.Create(nameof(EncoderFormatE).Split('.').Last(),typeof(EncoderFormatE))},
            };
            
            foreach (var item in types)
            {
                var instance = _factory.GetOrAddInstance<EncoderFormatA>(item.Value.Item1);
                Assert.That(instance.GetType(), Is.EqualTo(item.Value.Item2));
                Assert.That(instance.GetMsg(), Is.EqualTo($"Inside {item.Value.Item1}"));
            }
        }
        
        [Test]
        public void TestGetOrAddInstance_ShouldReturnInstances_ByTypeNameInAssemblyBasedOnConfiguration()
        {
            var cfgSection = new Dictionary<string, string>
            {
                
                { "EncoderOptions:Encoders:.abc", "EncoderFormatA"},
                { "EncoderOptions:Encoders:.bcd", "EncoderFormatB"},
                { "EncoderOptions:Encoders:.cde", "EncoderFormatC"},
                { "EncoderOptions:Encoders:.def", "EncoderFormatD"},
                { "EncoderOptions:Encoders:.efg", "EncoderFormatE"},
            };
            var actualTypes = new Dictionary<string, Type>
            {
                { ".abc", typeof(EncoderFormatA)},
                { ".bcd", typeof(EncoderFormatB)},
                { ".cde", typeof(EncoderFormatC)},
                { ".def", typeof(EncoderFormatD)},
                { ".efg", typeof(EncoderFormatE)},
            };

            var cfg = new ConfigurationBuilder()
                .AddInMemoryCollection(cfgSection)
                .Build();

            var t = cfg.ToString();
            
            var options = new EncoderOptions();
            cfg.Bind(nameof(EncoderOptions).Split(".").Last(),options);
            
            foreach (var item in options.Encoders)
            {
                var instance = _factory.GetOrAddInstance<EncoderFormatA>(item.Value);
                Assert.That(instance.GetType(), Is.EqualTo(actualTypes[item.Key]));
                Assert.That(instance.GetMsg(), Is.EqualTo($"Inside {options.Encoders[item.Key]}"));
            }
        }
    }
}