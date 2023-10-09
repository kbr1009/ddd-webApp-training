
namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Delete
{
    public class DeleteMinorWorkItemRequest
    {
        public string MinorWorkItemId { get; }
        public string DeletedBy { get; }

        public DeleteMinorWorkItemRequest(
            string minorWorkItemId, string deletedBy)
        { 
            MinorWorkItemId = minorWorkItemId;
            DeletedBy = deletedBy;
        }
    }
}
