
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Services.MajorWorkItems;

namespace TESTWebApp.UseCase.MajorWorkItems.Commands.Delete
{
    public class MajorWorkItemDeleteCommand : IMajorWorkItemDeleteCommand
    {
        private readonly IMajorWorkItemRepository _repository;

        public MajorWorkItemDeleteCommand(IMajorWorkItemRepository repository)
        { 
            _repository = repository;
        }

        public void Execute(DeleteMajorWorkItemRequest request)
        {
            MajorWorkItem majorWorkItem = _repository.FindByWorkItemId(request.MajorWorkItemId) 
                ?? throw new ArgumentException("指定された作業（大）項目は存在しないため削除できません。");

            MajorWorkItemService majorWorkItemService = new(_repository);
            if (majorWorkItemService.UsedAsRelatedData(majorWorkItem))
                throw new ArgumentException("作業記録に関連データとして使用されているため削除できません。非表示にする場合、使用禁止項目にしてください。");

            _repository.ExecuteDelWorkItem(majorWorkItem);
        }
    }
}
