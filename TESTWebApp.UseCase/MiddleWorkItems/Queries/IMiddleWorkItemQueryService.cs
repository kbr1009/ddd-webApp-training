
namespace TESTWebApp.UseCase.MiddleWorkItems.Queries
{
    public interface IMiddleWorkItemQueryService
    {
        IEnumerable<MiddleWorkItemDataResponse> GetAllMiddleWorkItem(string majorWorkItemId);
        IEnumerable<MiddleWorkItemDataResponse> GetMajorWorkItemsNotDel(string majorWorkItemId);
        MiddleWorkItemDataResponse GetMiddleWorkItem(string middleWorkItemId);
    }
}
