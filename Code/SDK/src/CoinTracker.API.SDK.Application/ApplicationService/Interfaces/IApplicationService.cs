using CoinTracker.API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.Application.ApplicationService.Interfaces
{
    public interface IApplicationService<TEntity, TDto, TRecivedDto> where TEntity : Entity
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetAsync(Guid id);
        Task<TDto> CreateAsync(TRecivedDto recivedDto);
        Task<IEnumerable<TDto>> CreateMultipleAsync(IEnumerable<TRecivedDto> recivedDtos);
        Task<TDto> UpdateAsync(Guid id, TRecivedDto recivedDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
