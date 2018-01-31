using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Web.Models;
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

        [DontWrapResult]
        public async Task<MetronicPagedResultDto<TEntityDto>> GetMetronicTable(TGetAllInput input)
        {
            var pagedResultDto = await GetAll(input);
            input.Total = pagedResultDto.TotalCount;
            return new MetronicPagedResultDto<TEntityDto>
            {
                Data = pagedResultDto.Items,
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
