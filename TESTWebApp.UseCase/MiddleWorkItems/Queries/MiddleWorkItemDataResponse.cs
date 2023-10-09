
namespace TESTWebApp.UseCase.MiddleWorkItems.Queries
{
    public class MiddleWorkItemDataResponse
    {
        public string MiddleWorkItemId { get; }
        public string MiddleWorkItemName { get; }
        public string CreatedUserName { get; }
        public string ModifiedUserName { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }
        public bool IsDeleted { get; }

        public MiddleWorkItemDataResponse(
            string middleWorkItemId,
            string middleWorkItemName,
            string createdUserName,
            string modifiedUserName,
            DateTime created,
            DateTime modified,
            bool isDeleted
            )
        {
            this.MiddleWorkItemId = middleWorkItemId;
            this.MiddleWorkItemName = middleWorkItemName;
            this.CreatedUserName = createdUserName;
            this.ModifiedUserName = modifiedUserName;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }
    }
}
