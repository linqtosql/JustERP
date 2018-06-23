using System.Collections.Generic;
using System.Threading.Tasks;
using JustERP.Application.User.Peoples.Dto;

namespace JustERP.Peoples
{
    public interface IPeopleAppService
    {
        /// <summary>
        /// 开始一个新的活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PeopleActivityDto> StartActivity(StartActivityInput input);

        /// <summary>
        /// 结束一个活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PeopleActivityDto> StopActivity(StopActivityInput input);
        /// <summary>
        /// 获取当前用户正在进行的活动
        /// </summary>
        /// <returns></returns>
        Task<PeopleActivityDto> GetCurrentActivity();
        /// <summary>
        /// 获取用户的活动详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PeopleActivityDto>> GetPeopleActivityHistory(GetActivityHistoryInput input);
        /// <summary>
        /// 添加一个活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ActivityDto> AddActivity(AddActivityInput input);
        /// <summary>
        /// 删除一个活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        Task DeleteActivity(long activityId);
        /// <summary>
        /// 修改活动名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangeActivityName(ChangeActivityNameInput input);
        /// <summary>
        /// 获取当前用户已使用的所有活动，没有不存在当前用户，则使用系统默认活动
        /// </summary>
        /// <returns></returns>
        Task<IList<ActivityDto>> GetUsedActivities();
        /// <summary>
        /// 获取当前用户未使用的所有活动
        /// </summary>
        /// <returns></returns>
        Task<IList<ActivityDto>> GetSystemActivities();

        /// <summary>
        /// 给正在进行的活动设置标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SetLabel(SetLabelInput input);
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <returns></returns>
        Task DeleteLabel(long labelId);
        /// <summary>
        /// 修改标签分类名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LabelCategoryDto> SetLabelCategoryName(SetLabelCategoryNameInput input);
        /// <summary>
        /// 获取所有标签分类
        /// </summary>
        /// <returns></returns>
        Task<IList<LabelCategoryDto>> GetLabelCategories();
    }
}