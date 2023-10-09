
namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Edit
{
    public class EditMinorWorkItemRequest
    {
        public string MinorWorkItemId { get; }
        public string MinorWorkItemName { get; }
        public string ModifiedBy { get; }
        public bool IsDeleted { get; }
        public EditMinorWorkItemRequest(
            string minorWorkItemId,
            string minorWorkItemName,
            string modifedBy,
            bool isDeleted) 
        { 
            MinorWorkItemId = minorWorkItemId;
            MinorWorkItemName = minorWorkItemName;
            ModifiedBy = modifedBy;
            IsDeleted = isDeleted;
        }
    }
}
