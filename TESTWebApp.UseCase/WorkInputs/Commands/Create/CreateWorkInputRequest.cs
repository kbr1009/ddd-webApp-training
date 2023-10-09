using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Commands.Create
{
    public sealed class CreateWorkInputRequest
    {
        public string UserId { get; }
        public string WebSessionId { get; }
        public string WorkItemId { get; }
        public string MiddleWorkItemId { get; }
        public string MinorWorkItemId { get; }
        public WorkStatus? Status { get; }

        public CreateWorkInputRequest(
            string userId,
            string webSessionId,
            string workItem,
            string middleWorkItemId,
            string minorWorkItemId,
            WorkStatus? workStatus
            )
        {
            UserId = userId;
            WebSessionId = webSessionId;
            WorkItemId = workItem;
            MiddleWorkItemId = middleWorkItemId;
            MinorWorkItemId = minorWorkItemId;
            Status = workStatus;
        }
    }
}
