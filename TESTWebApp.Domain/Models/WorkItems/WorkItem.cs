using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Models.WorkItems
{
    public class WorkItem
    {
        public WorkItemId WorkItemId { get; }
        public string WorkItemName { get; private set; }
        public UserId CreatedBy { get; private set; }
        public UserId ModifiedBy { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public WorkItem(
            WorkItemId workItemId,
            string workItemName, 
            UserId createUserId,
            UserId modifiedUserId,
            DateTime created, 
            DateTime modified, 
            bool isDeleted) 
        {
            this.WorkItemId = workItemId;
            this.WorkItemName = workItemName;
            this.CreatedBy = createUserId;
            this.ModifiedBy = modifiedUserId;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }

        public static WorkItem CreateNew(string workItemName, UserId createdBy)
        {
            var timeStamp = DateTime.Now;
            return new WorkItem(
                workItemId: WorkItemId.Generate(),
                workItemName: workItemName,
                createUserId: createdBy,
                modifiedUserId: createdBy,
                created: timeStamp,
                modified: timeStamp,
                isDeleted: false);
        }

        public void UpdateWorkItemName(string workItemName, UserId modifiedBy)
        {
            this.WorkItemName = workItemName;
            this.ModifiedBy = modifiedBy;
            this.Modified = DateTime.Now;
        }

        public void DelWorkItem(UserId modifiedBy)
        {
            if (this.IsDeleted)
            {
                return;
            }
            this.IsDeleted = true;
            this.ModifiedBy = modifiedBy;
            this.Modified = DateTime.Now;
        }
    }
}
