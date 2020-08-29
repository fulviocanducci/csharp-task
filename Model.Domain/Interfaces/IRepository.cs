using Model.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Model.Domain.Interfaces
{
    public interface IRepository<T> 
        where T: class, new()        
    {
        Task<T> AddAsync(T model);
        Task<bool> EditAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> RemoveAsync<TKey>(TKey id);
        Task<bool> RemoveAsync(T model);
        Task<T> GetAsync(params object[] keys);
        IAsyncEnumerable<T> GetAsync();
        Task<bool> SaveAsync();
    }
}
