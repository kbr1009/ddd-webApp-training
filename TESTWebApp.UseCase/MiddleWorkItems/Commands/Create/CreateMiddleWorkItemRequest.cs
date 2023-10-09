
namespace TESTWebApp.UseCase.MiddleWorkItems.Commands.Create
{
    public class CreateMiddleWorkItemRequest
    {
        public string MiddleWorkItemName { get; }
        public string MajorWorkItemId { get; }
        public string CreatedBy { get; }

        public CreateMiddleWorkItemRequest(
            string middleWorkItemName,
            string foreignKey,
            string createdBy)
        {
            this.MiddleWorkItemName = middleWorkItemName;
            this.MajorWorkItemId = foreignKey;
            this.CreatedBy = createdBy;
        }
    }
}
