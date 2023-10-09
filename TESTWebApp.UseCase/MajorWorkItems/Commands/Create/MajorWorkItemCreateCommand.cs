using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.MajorWorkItems;

namespace TESTWebApp.UseCase.MajorWorkItems.Commands.Create
{
    public sealed class MajorWorkItemCreateCommand : IMajorWorkItemCreateCommand
    {
        private readonly IMajorWorkItemRepository _repository;

        public MajorWorkItemCreateCommand(IMajorWorkItemRepository repository) 
        { 
            _repository = repository;
        }

        public void Execute(CreateMajorWorkItemRequest request)
        {
            MajorWorkItem majorWorkItem = MajorWorkItem.CreateNew(
                majorWorkItemName: new WorkItemName(request.MajorWorkItemName),
                createdBy: new UserId(request.CreatedBy));

            MajorWorkItemService workItemService = new(_repository);
            if (workItemService.IsDupulicatedWorkItem(majorWorkItem))
                throw new ArgumentException($"{request.MajorWorkItemName} はすでに登録されています。");

            _repository.SaveNewWorkItem(majorWorkItem);
        }
    }
}
