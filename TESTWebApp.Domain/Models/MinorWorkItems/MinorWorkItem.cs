using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Models.MinorWorkItems
{
    public sealed class MinorWorkItem
    {
        public MinorWorkItemId MinorWorkItemId { get; }
        public WorkItemName MinorWorkItemName { get; private set; }
        public MajorWorkItemId MajorWorkItemId { get; private set; }
        public MiddleWorkItemId MiddleWorkItemId { get; private set; }
        public UserId CreatedBy { get; private set; }
        public UserId ModifiedBy { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public MinorWorkItem(
            MinorWorkItemId minorWorkItemId,
            WorkItemName minorWorkItemName,
            MajorWorkItemId majorWorkItemId,
            MiddleWorkItemId middleWorkItemId,
            UserId createUserId,
            UserId modifiedUserId,
            DateTime created,
            DateTime modified,
            bool isDeleted)
        {
            this.MinorWorkItemId = minorWorkItemId;
            this.MinorWorkItemName = minorWorkItemName;
            this.MajorWorkItemId = majorWorkItemId;
            this.MiddleWorkItemId = middleWorkItemId;
            this.CreatedBy = createUserId;
            this.ModifiedBy = modifiedUserId;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }

        public static MinorWorkItem CreateNew(
            WorkItemName minorWorkItemName, 
            MajorWorkItemId rootForeignKey, 
            MiddleWorkItemId foreignKey, 
            UserId createdBy)
        {
            var timeStamp = DateTime.Now;
            return new MinorWorkItem(
                minorWorkItemId: new MinorWorkItemId(),
                minorWorkItemName: minorWorkItemName,
                majorWorkItemId: rootForeignKey,
                middleWorkItemId: foreignKey,
                createUserId: createdBy,
                modifiedUserId: createdBy,
                created: timeStamp,
                modified: timeStamp,
                isDeleted: false);
        }

        public void UpdateWorkItem(WorkItemName minorWorkItemName, bool isDeleted, UserId modifiedBy)
        {
            this.MinorWorkItemName = minorWorkItemName;
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
