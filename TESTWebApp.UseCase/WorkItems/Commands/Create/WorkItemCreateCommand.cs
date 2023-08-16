using TESTWebApp.Domain.Services.WorkItems;
using TESTWebApp.Domain.Models.WorkItems;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.UseCase.WorkItems.Commands.Create
{
    public sealed class WorkItemCreateCommand : IWorkItemCreateCommand
    {
        private readonly IWorkItemRepository _repository;

        public WorkItemCreateCommand(IWorkItemRepository repository) 
        { 
            _repository = repository;
        }

        public void Execute(CreateWorkItemRequest request)
        {
            WorkItem workItem = WorkItem.CreateNew(
                workItemName: request.WorkItemName, 
                createdBy: new UserId(request.CreatedBy));

            WorkItemService workItemService = new WorkItemService(_repository);
            if(workItemService.IsDupulicatedWorkItem(workItem))
                throw new ArgumentException($"'{request.WorkItemName}' はすでに登録されています。");

            _repository.SaveNewWorkItem(workItem);
        }
    }
}
