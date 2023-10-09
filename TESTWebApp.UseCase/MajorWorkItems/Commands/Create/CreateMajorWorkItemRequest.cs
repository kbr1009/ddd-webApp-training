
namespace TESTWebApp.UseCase.MajorWorkItems.Commands.Create
{
    public sealed class CreateMajorWorkItemRequest
    {
        public string MajorWorkItemName { get; }
        public string CreatedBy { get; }

        public CreateMajorWorkItemRequest(
            string majorWorkItemName, string createdBy)
        {
            MajorWorkItemName = majorWorkItemName;
            CreatedBy = createdBy;
        }
    }
}
