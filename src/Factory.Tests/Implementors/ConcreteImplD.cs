

namespace Factory.Tests.Implementors
{
    internal class ConcreteImplD : IImplementor
    {
        public string GetMsg()
        {
            return $"Inside {nameof(ConcreteImplD)}";

        }
    }
}
