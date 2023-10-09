
namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Create
{
    public class CreateMinorWorkItemRequest
    {
        public string MinorWorkItemName { get; }
        public string MajorWorkItemId { get; }
        public string MiddleWorkItemId { get; }
        public string CreatedBy { get; }

        public CreateMinorWorkItemRequest(
            string minorWorkItemName,
            string majorWorkItemId,
            string middleWorkItemId,
            string createdBy)
        {
            this.MinorWorkItemName = minorWorkItemName;
            this.MajorWorkItemId = majorWorkItemId;
            this.MiddleWorkItemId = middleWorkItemId;
            this.CreatedBy = createdBy;
        }
    }
}
