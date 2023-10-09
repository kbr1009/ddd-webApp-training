using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Services.Shared;

namespace TESTWebApp.Domain.Services.MiddleWorkItems
{
    public class MiddleWorkItemService : WorkItemsService<MiddleWorkItem>
    {
        private readonly IMiddleWorkItemRepository _repository;

        public MiddleWorkItemService(IMiddleWorkItemRepository repository)
        {
            _repository = repository;
        }

        public override bool IsDupulicatedWorkItem(MiddleWorkItem workItem)
        {
            var duplicatedWorkItem = _repository.FindByWorkItemName(
                workItem.MajorWorkItemId.Value, workItem.MiddleWorkItemName.Value);
            return duplicatedWorkItem != null;
        }

        public override bool IsDupulicatedWorkItemForEdit(
            MiddleWorkItem getDbWorkItem, WorkItemName editedWorkItemName)
        {
            // 編集した項目名た編集後の項目名に変化がない場合スキップする
            if (editedWorkItemName.Equals(getDbWorkItem.MiddleWorkItemName))
                return false;

            var duplicatedWorkItem = _repository.FindByWorkItemName(
                getDbWorkItem.MajorWorkItemId.Value, editedWorkItemName.Value);
            return duplicatedWorkItem != null;
        }

        public override bool UsedAsRelatedData(MiddleWorkItem workItem)
        {
            var count = _repository.CountsUsedInWorkInputTable(workItem.MiddleWorkItemId.Value);
            if (count > 0) return true;
            return false;
        }

        public override bool RelatedIsMismatch(MiddleWorkItem workItem)
        {
            throw new NotImplementedException();
        }
    }
}
