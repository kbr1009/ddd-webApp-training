using TESTWebApp.UseCase.WorkInputs.Data;

namespace TESTWebApp.UseCase.WorkInputs.Queries
{
    public interface IWorkInputDataQuery
    {
        IEnumerable<WorkInputData> FindAllWorkInputData(DateTime date);
        IEnumerable<WorkInputData> FindAllWorkInputData();
    }
}
