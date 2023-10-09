using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Services.Shared;

namespace TESTWebApp.Domain.Services.MajorWorkItems
{
    public class MajorWorkItemService : WorkItemsService<MajorWorkItem>
    {
        private readonly IMajorWorkItemRepository _repository;

        public MajorWorkItemService(IMajorWorkItemRepository repository)
        {
            _repository = repository;
        }

        public override bool IsDupulicatedWorkItem(MajorWorkItem workItem)
        {
            var duplicatedWorkItem = _repository.FindByWorkItemName(workItem.MajorWorkItemName.Value);
            return duplicatedWorkItem != null;
        }

        public override bool IsDupulicatedWorkItemForEdit(
            MajorWorkItem updatedWorkItem, WorkItemName EditedWorkItemName)
        {
            // 編集した項目名た編集後の項目名に変化がない場合スキップする
            if (updatedWorkItem.MajorWorkItemName.Equals(EditedWorkItemName))
                return false;

            var duplicatedWorkItem = _repository.FindByWorkItemName(EditedWorkItemName.Value);
            return duplicatedWorkItem != null;
        }

        public override bool UsedAsRelatedData(MajorWorkItem workItem)
        {
            var count = _repository.CountsUsedInWorkInputTable(workItem.WorkItemId.Value);
            if (count > 0) return true;
            return false;
        }

        public override bool RelatedIsMismatch(MajorWorkItem workItem)
        {
            throw new NotImplementedException();
        }
    }
}
