
namespace Smoothie.Domain.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        int Save(T item);
        int Delete(int id);
    }
}
