
namespace TESTWebApp.UseCase.MajorWorkItems.Queries
{
    public interface IMajorWorkItemQueryService
    {
        IEnumerable<MajorWorkItemDataResponse> GetAllMajorWorkItem();
        IEnumerable<MajorWorkItemDataResponse> GetMajorWorkItemsNotDel();
        MajorWorkItemDataResponse GetMajorWorkItem(string id);
    }
}
