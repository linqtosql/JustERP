using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using JustERP.MetronicTable.Dto;

namespace JustERP.MetronicTable
{
    public abstract class BaseMetronicTableAppService<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput> :
        AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, PagedResultRequestDto, TCreateInput, TUpdateInput>,
        IMetronicTableAppService<TEntityDto, MetronicPagedResultRequestDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected BaseMetronicTableAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }

        public async Task<MetronicPagedResultDto<TEntityDto>> GetMetronicTable(MetronicPagedResultRequestDto input)
        {
            var roles = await GetAll(input);
            input.Total = roles.TotalCount;
            return new MetronicPagedResultDto<TEntityDto>()
            {
                Data = roles.Items,
                Meta = input
            };
        }
    }
}
