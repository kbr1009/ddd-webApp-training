using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.Domain.Services.MajorWorkItems;

namespace TESTWebApp.Domain.Services.WorkInputs
{
    public class WorkInputService
    {
        private readonly IMajorWorkItemRepository _majorWorkItemRepository;

        public WorkInputService(IMajorWorkItemRepository majorWorkItemRepository)
        {
            _majorWorkItemRepository = majorWorkItemRepository;
        }

        public bool ExistWorkItem(WorkInput workInput)
        {
            MajorWorkItem majorWorkItem = _majorWorkItemRepository.FindByWorkItemId(workInput.MajorWorkItemId.Value);
            if(majorWorkItem is null)
                return false;
            return true;
        }
    }
}
