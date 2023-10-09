using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Models.MajorWorkItems
{
    public sealed class MajorWorkItem
    {
        public MajorWorkItemId WorkItemId { get; }
        public WorkItemName MajorWorkItemName { get; private set; }
        public UserId CreatedBy { get; private set; }
        public UserId ModifiedBy { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public MajorWorkItem(
            MajorWorkItemId workItemId,
            WorkItemName majorWorkItemName,
            UserId createUserId,
            UserId modifiedUserId,
            DateTime created,
            DateTime modified,
            bool isDeleted)
        {
            this.WorkItemId = workItemId;
            this.MajorWorkItemName = majorWorkItemName;
            this.CreatedBy = createUserId;
            this.ModifiedBy = modifiedUserId;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }

        public static MajorWorkItem CreateNew(WorkItemName majorWorkItemName, UserId createdBy)
        {
            var timeStamp = DateTime.Now;
            return new MajorWorkItem(
                workItemId: new MajorWorkItemId(),
                majorWorkItemName: majorWorkItemName,
                createUserId: createdBy,
                modifiedUserId: createdBy,
                created: timeStamp,
                modified: timeStamp,
                isDeleted: false);
        }

        public void UpdateWorkItem(WorkItemName workItemName, bool isDelete, UserId modifiedBy)
        {
            this.MajorWorkItemName = workItemName;
            this.IsDeleted = isDelete;
            this.ModifiedBy = modifiedBy;
            this.Modified = DateTime.Now;
        }

        public void DelWorkItem(UserId deletedBy)
        {
            if (this.IsDeleted) return;

            this.IsDeleted = true;
            this.ModifiedBy = deletedBy;
            this.Modified = DateTime.Now;
        }
    }
}
