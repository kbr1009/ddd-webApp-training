using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.WorkItems;

namespace TESTWebApp.Domain.Services.WorkItems
{
    public interface IWorkItemRepository
    {
        WorkItem FindByWorkItemName(string workItemName);

        void SaveNewWorkItem(WorkItem workItem);
    }
}
