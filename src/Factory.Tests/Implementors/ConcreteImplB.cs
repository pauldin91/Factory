

namespace Factory.Tests.Implementors
{
    internal class ConcreteImplB : IImplementor
    {
        public string GetMsg()
        {
            return $"Inside {nameof(ConcreteImplB)}";

        }
    }
}
