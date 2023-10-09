
namespace TESTWebApp.UseCase.MajorWorkItems.Queries
{
    public sealed class MajorWorkItemDataResponse
    {
        public string MajorWorkItemId { get; }
        public string MajorWorkItemName { get; }
        public string CreatedUserName { get; }
        public string ModifiedUserName { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }
        public bool IsDeleted { get; }

        public MajorWorkItemDataResponse(
            string majorWorkItemId, 
            string majorWorkItemName,
            string createdUserName,
            string modifiedUserName,
            DateTime created,
            DateTime modified,
            bool isDeleted
            ) 
        { 
            this.MajorWorkItemId = majorWorkItemId;
            this.MajorWorkItemName = majorWorkItemName;
            this.CreatedUserName = createdUserName;
            this.ModifiedUserName = modifiedUserName;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }
    }
}
