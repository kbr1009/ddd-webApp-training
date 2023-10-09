using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.MinorWorkItems;

namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Create
{
    public class MinorWorkItemCreateCommand : IMinorWorkItemCreateCommand
    {
        private readonly IMinorWorkItemRepository _repository;

        public MinorWorkItemCreateCommand(IMinorWorkItemRepository repository) 
        {
            _repository = repository;
        }

        public void Execute(CreateMinorWorkItemRequest request) 
        {
            MinorWorkItem minorWorkItem = MinorWorkItem.CreateNew(
                minorWorkItemName: new WorkItemName(request.MinorWorkItemName),
                rootForeignKey: new MajorWorkItemId(request.MiddleWorkItemId),
                foreignKey: new MiddleWorkItemId(request.MiddleWorkItemId),
                createdBy: new UserId(request.CreatedBy));

            MinorWorkItemService minorWorkItemService = new(_repository);
            if (minorWorkItemService.IsDupulicatedWorkItem(minorWorkItem))
                throw new ArgumentException($"'{request.MinorWorkItemName}' はすでに登録されています。");

            // 最上位親データのバリデーション
            //if ()
            //    throw new ArgumentException();

            // 親データのバリデーション
            //if ()
            //    throw new ArgumentException();

            _repository.SaveNewWorkItem(minorWorkItem);
        }
    }
}
