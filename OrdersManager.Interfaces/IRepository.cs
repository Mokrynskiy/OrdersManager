using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersManager.Interfaces
{
    public interface IRepository<T> where T: class, IEntity, new()
    {
        IQueryable<T> Items { get; }
        T GetById(int id);
        Task<T> GetByIdAcync(int id, CancellationToken Cancel = default);
        T Add(T item);
        Task<T> AddAsync(T item, CancellationToken Cancel = default);
        void Update(T item);
        Task UpdateAsync(T item, CancellationToken Cancel = default);
        void Remove(int id);
        Task RemoveAsync(int id, CancellationToken Cancel = default);
    }
}
