using TESTWebApp.Domain.Models.MiddleWorkItems;

namespace TESTWebApp.Domain.Services.MiddleWorkItems
{
    public interface IMiddleWorkItemRepository
    {
        MiddleWorkItem FindByWorkItemName(string foreignKey, string itemName);
        MiddleWorkItem FindById(string id);
        int CountsUsedInWorkInputTable(string id);
        void SaveNewWorkItem(MiddleWorkItem item);
        void ExecuteDelWorkItem(MiddleWorkItem item);
        void ExecuteUpdateWorkItem(MiddleWorkItem item);
    }
}
