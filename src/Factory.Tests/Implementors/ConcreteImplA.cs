using Factory.Interfaces;

namespace Factory.Tests.Implementors
{
    internal class ConcreteImplA : IImplementor
    {
        public string GetMsg()
        {
            return $"Inside {nameof(ConcreteImplA)}";
        }
    }
}