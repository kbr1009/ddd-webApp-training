
namespace TESTWebApp.UseCase.MinorWorkItems.Queries
{
    public class MinorWorkItemDataResponse
    {
        public string MinorWorkItemId { get; }
        public string MinorWorkItemName { get; }
        public string CreatedUserName { get; }
        public string ModifiedUserName { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }
        public bool IsDeleted { get; }

        public MinorWorkItemDataResponse(
            string minorWorkItemId,
            string minorWorkItemName,
            string createdUserName,
            string modifiedUserName,
            DateTime created,
            DateTime modified,
            bool isDeleted
            )
        {
            this.MinorWorkItemId = minorWorkItemId;
            this.MinorWorkItemName = minorWorkItemName;
            this.CreatedUserName = createdUserName;
            this.ModifiedUserName = modifiedUserName;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }
    }
}
