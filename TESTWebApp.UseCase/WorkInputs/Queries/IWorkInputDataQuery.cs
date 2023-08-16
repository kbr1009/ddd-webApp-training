
namespace TESTWebApp.UseCase.WorkInputs.Queries
{
    public interface IWorkInputDataQuery
    {
        IEnumerable<WorkInputDataResponse> FindAllWorkInputData();
        IEnumerable<WorkInputDataResponse> FindAllWorkInputData(DateTime date);
        IEnumerable<WorkInputDataResponse> FindAllWorkInputData(string userId);
    }
}
