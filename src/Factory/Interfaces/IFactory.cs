
namespace Factory.Interfaces
{
    public interface IFactory
    {
        IReadOnlyDictionary<Type, IImplementor> Cache { get; }

        IImplementor GetInstance(Type type);
    }
}