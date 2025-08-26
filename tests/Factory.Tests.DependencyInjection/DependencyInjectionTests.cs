using Factory.DependencyInjection;
using Factory.Interfaces;
using Factory.Tests.Common;
using Factory.Tests.Common.Models.Encoders;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace Factory.Tests.DependencyInjection
{
    public class DependencyInjectionTests : IDisposable
    {
        private readonly IServiceProvider _sp;
        private readonly HashSet<Type> _types =
        [
            typeof(EncoderFormatA), typeof(EncoderFormatB), typeof(EncoderFormatC), typeof(EncoderFormatD),
            typeof(EncoderFormatE)
        ];

        public DependencyInjectionTests()
        {
            _sp = new ServiceCollection()
            .AddFactory<IEncoder,EncoderFormatA>()
            .BuildServiceProvider();

            var factory = _sp.GetRequiredService<IFactory<IEncoder>>();

            foreach (var type in _types)
            {
                _ = factory.GetOrAddInstance(type);
            }
        }

        public void Dispose() { }
        
        [Test]
        public void TestRegistrationWithAssembly()
        {
            var factory = _sp.GetRequiredService<IFactory<IEncoder>>();
            foreach (var type in _types)
            {
                var concrete = factory.GetOrAddInstance(type);
                Assert.That(type, Is.EqualTo(concrete.GetType()));
            }
        }

        [Test]
        public void TestThatFactoryCachesAllImplementators()
        {
            var factory = _sp.GetRequiredService<IFactory<IEncoder>>();
            Assert.That(factory.All(s=> _types.Contains(s.GetType())), Is.True);
        }
    }
}