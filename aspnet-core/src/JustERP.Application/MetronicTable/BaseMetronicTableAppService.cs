using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using JustERP.MetronicTable.Dto;

namespace JustERP.MetronicTable
{
    public abstract class BaseMetronicTableAppService<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput> :
        BaseMetronicTableAppService<TEntity, TEntityDto, TPrimaryKey, MetronicPagedResultRequestDto, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected BaseMetronicTableAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }
    }

    public abstract class BaseMetronicTableAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> :
        AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>,
        IMetronicTableAppService<TEntityDto, TGetAllInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetAllInput : MetronicPagedResultRequestDto
    {
        protected BaseMetronicTableAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }

        public async Task<MetronicPagedResultDto<TEntityDto>> GetMetronicTable(TGetAllInput input)
        {
            var roles = await GetAll(input);
            input.Total = roles.TotalCount;
            return new MetronicPagedResultDto<TEntityDto>
            {
                Data = roles.Items,
                Meta = input
            };
        }
    }

    public abstract class BaseMetronicTableAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput> :
        BaseMetronicTableAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TGetAllInput : MetronicPagedResultRequestDto
    {
        protected BaseMetronicTableAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }
    }
}
