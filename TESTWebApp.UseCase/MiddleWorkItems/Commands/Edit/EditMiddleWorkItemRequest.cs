
namespace TESTWebApp.UseCase.MiddleWorkItems.Commands.Edit
{
    public class EditMiddleWorkItemRequest
    {
        public string MiddleWorkItemId { get; }
        public string MiddleWorkItemName { get; }
        public string ModifiedBy { get; }
        public bool IsDeleted { get; }

        public EditMiddleWorkItemRequest(
            string middleWorkItemId,
            string middleWorkItemName,
            string modifedBy,
            bool isDeleted)
        {
            MiddleWorkItemId = middleWorkItemId;
            MiddleWorkItemName = middleWorkItemName;
            ModifiedBy = modifedBy;
            IsDeleted = isDeleted;
        }
    }
}
