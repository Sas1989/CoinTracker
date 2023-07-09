using API.SDK.Application.ApplicationService;
using API.SDK.Application.DataMapper;
using API.SDK.Application.Repository;
using API.SDK.Domain.Entities;

namespace API.SDK.Infrastructure.Services
{
    public class ApplicationService<TEntity, TDto, TDtoIn> : IApplicationService<TEntity, TDto, TDtoIn> where TEntity : Entity
    {
        protected readonly IRepository<TEntity> repository;
        protected readonly IDataMapper mapper;

        public ApplicationService(IRepository<TEntity> repository, IDataMapper mapper)
        {
            this.repository= repository;
            this.mapper = mapper;
        }

        public async Task<TDto> CreateAsync(TDtoIn recivedDto)
        {
            CheckRecivedDtoNotNull(recivedDto);
            TEntity entity = ToEntity(recivedDto);
            var insertEntity = await repository.CreateAsync(entity);
            return ToDto(insertEntity);
        }

        public async Task<IEnumerable<TDto>> CreateMultipleAsync(IEnumerable<TDtoIn> recivedDtos)
        {
            var entities = ToEntity(recivedDtos);
            var insertEntities = await repository.CreateAsync(entities);
            return ToDto(insertEntities);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> coins = await repository.GetAllAsync();
            return ToDto(coins);
        }

        public async Task<TDto?> GetAsync(Guid id)
        {
            var entity = await repository.GetAsync(id);

            return ToDto(entity);
        }

        public async Task<TDto?> UpdateAsync(Guid id, TDtoIn recivedDto)
        {
            CheckRecivedDtoNotNull(recivedDto);

            var entity = ToEntity(recivedDto);
            entity.Id = id;

            var entityUpdated = await repository.UpdateAsync(entity);

            return ToDto(entityUpdated);
        }

        private static void CheckRecivedDtoNotNull(TDtoIn recivedDto)
        {
            if (recivedDto is null || recivedDto.Equals(default(TDtoIn)))
            {
                throw new ArgumentNullException(nameof(recivedDto));
            }
        }
        protected TDto ToDto(TEntity? entity)
        {
            return mapper.Map<TDto>(entity);
        }

        protected IEnumerable<TDto> ToDto(IEnumerable<TEntity> entity)
        {
            return mapper.Map<IEnumerable<TDto>>(entity);
        }

        protected TEntity ToEntity(TDtoIn recivedDto)
        {
            return mapper.Map<TEntity>(recivedDto);
        }

        protected IEnumerable<TEntity> ToEntity(IEnumerable<TDtoIn> recivedDto)
        {
            return mapper.Map<IEnumerable<TEntity>>(recivedDto);
        }

    }
}
