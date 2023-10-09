using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Domain.Services.MinorWorkItems;

namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Delete
{
    public class MinorWorkItemDeleteCommand : IMinorWorkItemDeleteCommand
    {
        private readonly IMinorWorkItemRepository _repository;

        public MinorWorkItemDeleteCommand(IMinorWorkItemRepository repository)
        {
            _repository = repository;
        }

        public void Execute(DeleteMinorWorkItemRequest request)
        {
            MinorWorkItem minorWorkItem = _repository.FindById(request.MinorWorkItemId)
                ?? throw new ArgumentException("指定された作業（小）項目は存在しないため削除できません。");

            MinorWorkItemService minorWorkItemService = new(_repository);
            if (minorWorkItemService.UsedAsRelatedData(minorWorkItem))
                throw new ArgumentException("作業記録に関連データとして使用されているため削除できません。非表示にする場合、使用禁止項目にしてください。");

            _repository.ExecuteDelWorkItem(minorWorkItem);
        }
    }
}
