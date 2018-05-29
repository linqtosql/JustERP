namespace Abp.Domain.Entities.Auditing
{
    public interface ILocalizationAudited
    {
        string Language { get; set; }
    }
}