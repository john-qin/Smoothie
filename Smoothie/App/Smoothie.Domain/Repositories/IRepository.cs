
namespace Smoothie.Domain.Repositories
{
    public interface IRepository<T, TPK>
    {
        T Get(TPK id);
        int Save(T item);
        int Delete(TPK id);
    }
}
