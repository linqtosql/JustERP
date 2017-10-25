using System.Threading.Tasks;
using JustERP.MetronicTable.Dto;

namespace JustERP.MetronicTable
{
    public interface IMetronicTableAppService<TEntityDto, in TInput> where TInput : MetronicPagedResultRequestDto
    {
        Task<MetronicPagedResultDto<TEntityDto>> GetMetronicTable(TInput input);
    }
}