
namespace Factory.Tests.Implementors
{
    internal class ConcreteImplC : IImplementor
    {
        public string GetMsg()
        {
            return $"Inside {nameof(ConcreteImplC)}";

        }
    }
}
