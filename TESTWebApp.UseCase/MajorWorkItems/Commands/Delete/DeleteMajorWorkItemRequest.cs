
namespace TESTWebApp.UseCase.MajorWorkItems.Commands.Delete
{
    public class DeleteMajorWorkItemRequest
    {
        public string MajorWorkItemId { get; }
        public string DeletedBy { get; }
        public DeleteMajorWorkItemRequest(
            string majorWorkItemId, string deletedBy)
        { 
            this.MajorWorkItemId = majorWorkItemId;
            this.DeletedBy = deletedBy;
        }
    }
}
