using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Services.MiddleWorkItems;

namespace TESTWebApp.UseCase.MiddleWorkItems.Commands.Delete
{
    public class MiddleWorkItemDeleteCommand : IMiddleWorkItemDeleteCommand
    {
        private readonly IMiddleWorkItemRepository _repository;

        public MiddleWorkItemDeleteCommand(IMiddleWorkItemRepository repository)
        { 
            _repository = repository;
        }

        public void Execute(DeleteMiddleWorkItemRequest request)
        { 
            MiddleWorkItem middleWorkItem = _repository.FindById(request.MiddleWorkItemId)
                ?? throw new ArgumentException("指定された作業（中）項目は存在しないため削除できません。");

            MiddleWorkItemService middleWorkItemService = new(_repository);
            if (middleWorkItemService.UsedAsRelatedData(middleWorkItem))
                throw new ArgumentException("作業記録に関連データとして使用されているため削除できません。非表示にする場合、使用禁止項目にしてください。");

            _repository.ExecuteDelWorkItem(middleWorkItem);
        }
    }
}
