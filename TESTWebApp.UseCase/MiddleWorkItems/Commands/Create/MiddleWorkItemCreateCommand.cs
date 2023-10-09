using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.MajorWorkItems;
using TESTWebApp.Domain.Services.MiddleWorkItems;

namespace TESTWebApp.UseCase.MiddleWorkItems.Commands.Create
{
    /// <summary>
    /// 作業項目中項目を作成するユースケース
    /// </summary>
    public sealed class MiddleWorkItemCreateCommand : IMiddleWorkItemCreateCommand
    {
        private readonly IMiddleWorkItemRepository _repository;

        public MiddleWorkItemCreateCommand(IMiddleWorkItemRepository repository) 
        { 
            _repository = repository;
        }

        public void Execute(CreateMiddleWorkItemRequest request) 
        {
            MiddleWorkItem middleWorkItem = MiddleWorkItem.CreateNew(
                middleWorkItemName: new WorkItemName(request.MiddleWorkItemName),
                foreignKey: new MajorWorkItemId(request.MajorWorkItemId),
                createdBy: new UserId(request.CreatedBy));

            MiddleWorkItemService middleWorkItemService = new(_repository);
            if (middleWorkItemService.IsDupulicatedWorkItem(middleWorkItem))
                throw new ArgumentException($"'{request.MiddleWorkItemName}' はすでに登録されています。");

            // 親データのバリデーション
            //if ()
            //    throw new ArgumentException();

            _repository.SaveNewWorkItem(middleWorkItem);
        }
    }
}
