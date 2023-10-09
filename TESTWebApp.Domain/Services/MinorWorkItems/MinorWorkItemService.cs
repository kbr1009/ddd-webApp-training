using TESTWebApp.Domain.Services.Shared;
using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Domain.Models.MajorWorkItems;

namespace TESTWebApp.Domain.Services.MinorWorkItems
{
    public class MinorWorkItemService : WorkItemsService<MinorWorkItem>
    {
        private readonly IMinorWorkItemRepository _repository;

        public MinorWorkItemService(IMinorWorkItemRepository repository)
        {
            _repository = repository;
        }

        public override bool IsDupulicatedWorkItem(MinorWorkItem from)
        {
            var duplicatedWorkItem = _repository.FindByWorkItemName(
                from.MiddleWorkItemId.Value, from.MinorWorkItemName.Value);
            return duplicatedWorkItem != null;
        }

        public override bool IsDupulicatedWorkItemForEdit(
            MinorWorkItem getDbWorkItem, WorkItemName editedWorkItemName)
        {
            // 編集した項目名た編集後の項目名に変化がない場合スキップする
            if (editedWorkItemName.Equals(getDbWorkItem.MinorWorkItemName))
                return false;

            var duplicatedWorkItem = _repository.FindByWorkItemName(
                getDbWorkItem.MiddleWorkItemId.Value, editedWorkItemName.Value);
            return duplicatedWorkItem != null;
        }

        public override bool UsedAsRelatedData(MinorWorkItem from)
        {
            var count = _repository.CountsUsedInWorkInputTable(from.MinorWorkItemId.Value);
            if (count > 0) return true;
            return false;
        }

        public override bool RelatedIsMismatch(MinorWorkItem from)
        {
            throw new NotImplementedException("未実装");
        }
    }
}
