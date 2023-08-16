
namespace TESTWebApp.UseCase.WorkItems.Commands.Create
{
    public sealed class CreateWorkItemRequest
    {
        public string WorkItemName { get; }
        public string CreatedBy { get; }

        public CreateWorkItemRequest(string workItemName, string createdBy)
        {
            WorkItemName = workItemName;
            CreatedBy = createdBy;
        }
    }
}
