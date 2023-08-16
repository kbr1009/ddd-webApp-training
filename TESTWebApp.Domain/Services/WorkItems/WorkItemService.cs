using TESTWebApp.Domain.Models.WorkItems;

namespace TESTWebApp.Domain.Services.WorkItems
{
    public sealed class WorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemService(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public bool IsDupulicatedWorkItem(WorkItem workItem)
        {
            var duplicatedWorkItem = _workItemRepository.FindByWorkItemName(workItem.WorkItemName);
            return duplicatedWorkItem != null;
        }
    }
}
