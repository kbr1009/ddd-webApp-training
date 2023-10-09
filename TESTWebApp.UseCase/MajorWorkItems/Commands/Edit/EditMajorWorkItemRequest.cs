
namespace TESTWebApp.UseCase.MajorWorkItems.Commands.Edit
{
    public class EditMajorWorkItemRequest
    {
        public string MajorWorkItemId { get; }
        public string MajorWorkItemName { get; }
        public string ModifiedBy { get; }
        public bool IsDeleted { get; }

        public EditMajorWorkItemRequest(
            string majorWorkItemId,
            string majorWorkItemName,
            string modifiedBy,
            bool isDeleted)
        {
            this.MajorWorkItemId = majorWorkItemId;
            this.MajorWorkItemName = majorWorkItemName;
            this.ModifiedBy = modifiedBy;
            this.IsDeleted = isDeleted;
        }
    }
}
