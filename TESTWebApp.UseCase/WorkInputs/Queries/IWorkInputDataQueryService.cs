
namespace TESTWebApp.UseCase.WorkInputs.Queries
{
    public interface IWorkInputDataQueryService
    {
        IEnumerable<WorkInputDataResponse> FindWorkInputDataForToday(string userId, DateTime today);
        WorkInputDataResponse GetCurrentWork(string userId, DateTime today);
    }
}
