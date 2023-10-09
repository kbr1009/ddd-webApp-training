using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Models.MiddleWorkItems
{
    public sealed class MiddleWorkItem
    {
        public MiddleWorkItemId MiddleWorkItemId { get; }
        public WorkItemName MiddleWorkItemName { get; private set; }
        public MajorWorkItemId MajorWorkItemId { get; private set; }
        public UserId CreatedBy { get; private set; }
        public UserId ModifiedBy { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public MiddleWorkItem(
            MiddleWorkItemId middleWorkItemId,
            MajorWorkItemId majorWorkItemId,
            WorkItemName middleWorkItemName,
            UserId createUserId,
            UserId modifiedUserId,
            DateTime created,
            DateTime modified,
            bool isDeleted)
        {
            this.MiddleWorkItemId = middleWorkItemId;
            this.MajorWorkItemId = majorWorkItemId;
            this.MiddleWorkItemName = middleWorkItemName;
            this.CreatedBy = createUserId;
            this.ModifiedBy = modifiedUserId;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }

        public static MiddleWorkItem CreateNew(
            WorkItemName middleWorkItemName,MajorWorkItemId foreignKey, UserId createdBy)
        {
            var timeStamp = DateTime.Now;
            return new MiddleWorkItem(
                middleWorkItemId: new MiddleWorkItemId(),
                majorWorkItemId: foreignKey,
                middleWorkItemName: middleWorkItemName,
                createUserId: createdBy,
                modifiedUserId: createdBy,
                created: timeStamp,
                modified: timeStamp,
                isDeleted: false);
        }

        public void UpdateWorkItem(WorkItemName workItemName, bool isDeleted, UserId modifiedBy)
        {
            this.MiddleWorkItemName = workItemName;
            this.IsDeleted = isDeleted;
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
