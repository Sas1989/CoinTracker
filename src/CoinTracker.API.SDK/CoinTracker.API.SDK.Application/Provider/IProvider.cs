using CoinTracker.API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.Application.IProvider
{
    public interface IProvider<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAsync<TField>(string field, TField filedValue);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
