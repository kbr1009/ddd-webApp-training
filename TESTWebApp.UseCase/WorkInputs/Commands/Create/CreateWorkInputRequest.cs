using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Commands.Create
{
    public sealed class CreateWorkInputRequest
    {
        public string UserId { get; }
        public string WorkItem { get; }
        public WorkStatus? Status { get; }

        public CreateWorkInputRequest(
            string userId,
            string workItem,
            WorkStatus? workStatus
            )
        {
            UserId = userId;
            WorkItem = workItem;
            Status = workStatus;
        }
    }
}
