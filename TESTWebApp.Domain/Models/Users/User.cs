using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.Users
{
    public sealed class User
    {
        public UserId UserId { get; }
        public UserName UserName { get; private set; }
        public UserId CreatedBy { get; private set; }
        public UserId ModifiedBy { get; private set; }
        public WebSessionId WebSessionId { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public User(
            UserId userId, 
            UserName UserName, 
            UserId createdBy,
            UserId modifiedBy,
            WebSessionId webSessionId,
            DateTime created, 
            DateTime modified, 
            bool isDeleted) 
        { 
            this.UserId = userId;
            this.UserName = UserName;
            this.CreatedBy = createdBy;
            this.ModifiedBy = modifiedBy;
            this.WebSessionId = webSessionId;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }

        public static User CreateNew(UserName userName, UserId createdBy)
        {
            var timeStamp = DateTime.Now;
            return new User(
                userId: new UserId(),
                UserName: userName, 
                createdBy: createdBy,
                modifiedBy: createdBy,
                webSessionId: new WebSessionId(),
                created: timeStamp,
                modified: timeStamp, 
                isDeleted: false);
        }

        public void UpdateUserName(UserName userName, UserId modifiedBy)
        {
            this.UserName = userName;
            this.ModifiedBy = modifiedBy;
            this.Modified = DateTime.Now;
        }

        public void UpdateUserLoginSession(WebSessionId webSessionId)
        {
            this.WebSessionId = webSessionId;
        }

        public void DelUser(UserId modifiedBy)
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
