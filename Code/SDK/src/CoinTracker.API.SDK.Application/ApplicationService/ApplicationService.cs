using CoinTracker.API.SDK.Application.ApplicationService.Interfaces;
using CoinTracker.API.SDK.Application.DataMapper;
using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.SDK.Domain.Entities;
using CoinTracker.API.SDK.UnitTests.System.Application.ApplicationService;

namespace CoinTracker.API.SDK.Application.ApplicationService
{
    public class ApplicationService<TEntity, TDto, TRecivedDto> : IApplicationService<TEntity, TDto, TRecivedDto> where TEntity : Entity
    {
        protected readonly IProvider<TEntity> provider;
        protected readonly IDataMapper mapper;

        public ApplicationService(IProvider<TEntity> provider, IDataMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        public async Task<TDto> CreateAsync(TRecivedDto recivedDto)
        {
            CheckRecivedDtoNotNull(recivedDto);
            TEntity entity = ToEntity(recivedDto);
            var insertEntity = await provider.CreateAsync(entity);
            return ToDto(insertEntity);
        }

        public async Task<IEnumerable<TDto>> CreateMultipleAsync(IEnumerable<TRecivedDto> recivedDtos)
        {
            var entities = ToEntity(recivedDtos);
            var insertEntities = await provider.CreateAsync(entities);
            return ToDto(insertEntities);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await provider.DeleteAsync(id);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> coins = await provider.GetAllAsync();
            return ToDto(coins);
        }

        public async Task<TDto> GetAsync(Guid id)
        {
            var entity = await provider.GetAsync(id);

            CheckEntityIsNotNull(entity);

            return ToDto(entity);
        }

        public async Task<TDto> UpdateAsync(Guid id, TRecivedDto recivedDto)
        {
            CheckRecivedDtoNotNull(recivedDto);

            var entity = ToEntity(recivedDto);
            entity.Id = id;

            var entityUpdated = await provider.UpdateAsync(entity);

            CheckEntityIsNotNull(entityUpdated);

            return ToDto(entity);
        }

        private static void CheckRecivedDtoNotNull(TRecivedDto recivedDto)
        {
            if (recivedDto is null)
            {
                throw new ArgumentNullException(nameof(recivedDto));
            }
        }
        protected TDto ToDto(TEntity entity)
        {
            return mapper.Map<TDto>(entity);
        }

        protected IEnumerable<TDto> ToDto(IEnumerable<TEntity> entity)
        {
            return mapper.Map<IEnumerable<TDto>>(entity);
        }

        protected TEntity ToEntity(TRecivedDto recivedDto)
        {
            return mapper.Map<TEntity>(recivedDto);
        }

        protected IEnumerable<TEntity> ToEntity(IEnumerable<TRecivedDto> recivedDto)
        {
            return mapper.Map<IEnumerable<TEntity>>(recivedDto);
        }

        protected static void CheckEntityIsNotNull(TEntity entity)
        {
            if (entity == default(TEntity))
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
