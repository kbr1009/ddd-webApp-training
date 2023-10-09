
namespace TESTWebApp.UseCase.MiddleWorkItems.Commands.Delete
{
    public class DeleteMiddleWorkItemRequest
    {
        public string MiddleWorkItemId { get; }
        public string DeletedBy { get; }

        public DeleteMiddleWorkItemRequest(
            string middleWorkItemId, string deletedBy)
        {
            MiddleWorkItemId = middleWorkItemId;
            DeletedBy = deletedBy;
        }
    }
}
