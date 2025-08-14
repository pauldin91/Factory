using Factory.Interfaces;

namespace Factory.Tests.Implementors
{
    internal class ConcreteImplE : IImplementor
    {
        public string GetMsg()
        {
            return $"Inside {nameof(ConcreteImplE)}";

        }
    }
}