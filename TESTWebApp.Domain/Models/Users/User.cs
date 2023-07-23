using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TESTWebApp.Domain.Models.Users
{
    public class User
    {
        public UserId UserId { get; }
        public string UserName { get; private set; }
        public UserId CreatedBy { get; private set; }
        public UserId ModifiedBy { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public User(
            UserId userId, 
            string UserName, 
            UserId createdBy,
            UserId modifiedBy,
            DateTime created, 
            DateTime modified, 
            bool isDeleted) 
        { 
            this.UserId = userId;
            this.UserName = UserName;
            this.CreatedBy = createdBy;
            this.ModifiedBy = modifiedBy;
            this.Created = created;
            this.Modified = modified;
            this.IsDeleted = isDeleted;
        }

        public static User CreateNew(string UserName, UserId createdBy)
        {
            var timeStamp = DateTime.Now;
            return new User(
                userId: UserId.Generate(),
                UserName: UserName, 
                createdBy: createdBy,
                modifiedBy: createdBy,
                created: timeStamp,
                modified: timeStamp, 
                isDeleted: false);
        }

        public void UpdateUserName(string UserName, UserId modifiedBy)
        {
            this.UserName = UserName;
            this.ModifiedBy = modifiedBy;
            this.Modified = DateTime.Now;
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
