using TESTWebApp.Domain.Models.MinorWorkItems;

namespace TESTWebApp.Domain.Services.MinorWorkItems
{
    public interface IMinorWorkItemRepository
    {
        MinorWorkItem FindByWorkItemName(string foreignKey, string workItemName);
        MinorWorkItem FindById(string id);
        int CountsUsedInWorkInputTable(string id);
        void SaveNewWorkItem(MinorWorkItem item);
        void ExecuteUpdateWorkItem(MinorWorkItem item);
        void ExecuteDelWorkItem(MinorWorkItem item);
    }
}
