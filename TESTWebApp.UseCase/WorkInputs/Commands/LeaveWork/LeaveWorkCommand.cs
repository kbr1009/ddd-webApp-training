
using TESTWebApp.Domain.Services.MajorWorkItems;
using TESTWebApp.Domain.Services.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Commands.LeaveWork
{
    public class LeaveWorkCommand : ILeaveWorkCommand
    {
        private readonly IWorkInputRepository _workInputRepository;
        private readonly IMajorWorkItemRepository _majorWorkItemRepository;

        public LeaveWorkCommand(
            IWorkInputRepository workInputRepository,
            IMajorWorkItemRepository majorWorkItemRepository)
        { 
            _workInputRepository = workInputRepository;
            _majorWorkItemRepository = majorWorkItemRepository;
        }

        public void Execute(LeaveWorkRequest request)
        { 
            throw new NotImplementedException();
        }
    }
}
