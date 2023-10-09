using TESTWebApp.Domain.Models.MajorWorkItems;

namespace TESTWebApp.Domain.Services.MajorWorkItems
{
    public interface IMajorWorkItemRepository
    {
        MajorWorkItem FindByWorkItemName(string workItemName);
        MajorWorkItem FindByWorkItemId(string workItemId);
        int CountsUsedInWorkInputTable(string workItemId);
        void SaveNewWorkItem(MajorWorkItem workItem);
        void ExecuteDelWorkItem(MajorWorkItem workItem);
        void ExecuteUpdateWorkItem(MajorWorkItem workItem);
    }
}
