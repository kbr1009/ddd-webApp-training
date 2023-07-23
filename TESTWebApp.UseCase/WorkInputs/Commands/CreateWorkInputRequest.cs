using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Commands
{
    public class CreateWorkInputRequest
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
