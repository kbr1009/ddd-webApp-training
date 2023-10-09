
using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Commands.LeaveWork
{
    public class LeaveWorkRequest
    {
        public string UserId { get; }
        public string WorkItemId { get; }
        public string MiddleWorkItemId { get; }
        public string MinorWorkItemId { get; }
        public WorkStatus? Status { get; }

        public LeaveWorkRequest(
            string userId,
            string workItem,
            string middleWorkItemId,
            string minorWorkItemId,
            WorkStatus? workStatus
            )
        {
            UserId = userId;
            WorkItemId = workItem;
            MiddleWorkItemId = middleWorkItemId;
            MinorWorkItemId = minorWorkItemId;
            Status = workStatus;
        }
    }
}
