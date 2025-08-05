
namespace Factory.Interfaces
{
    public interface IFactory : IEnumerable<IImplementor>
    {

        IImplementor GetInstance(Type type);
    }
}