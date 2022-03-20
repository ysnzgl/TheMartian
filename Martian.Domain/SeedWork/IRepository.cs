using System.Threading.Tasks;

namespace Martian.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task SaveAsync(T entity);
        Task<T> GetAsync(string id);
        Task DeleteAllAsync();

    }
}
