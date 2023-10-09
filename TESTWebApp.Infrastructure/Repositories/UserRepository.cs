using Microsoft.EntityFrameworkCore;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.Users;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Infrastructure.Database.Tables;


namespace TESTWebApp.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly AppDbContext _database;

        public UserRepository(AppDbContext database)
        {
            _database = database;
        }

        public User FindByUserName(string userName)
        {
            USER userDataModel = _database.UserDataModels
                .Where(x => x.UserName == userName)
                .FirstOrDefault();

            if (userDataModel == null)
                return null;

            return ToEntity(userDataModel);
        }

        public User FindByUserId(string id)
        {
            USER userDataModel = _database.UserDataModels
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (userDataModel == null)
                return null;

            return ToEntity(userDataModel);
        }

        public void SaveNewUser(User newUser)
        {
            _database.UserDataModels.Add(ToDBModel(newUser));
            _database.SaveChanges();
        }

        public void UpdateWebSession(User user)
        {
            var userDBModel = ToDBModel(user);
            var userDBData = _database.UserDataModels.Where(x => x.Id == user.UserId.Value).FirstOrDefault();
            userDBData.WebSession = userDBModel.WebSession;
            _database.SaveChanges();
        }

        public void UpdateWebSession(UserId userId, WebSessionId newWebSessionId)
        {
            var userDBData = _database.UserDataModels.Where(x => x.Id == userId.Value).FirstOrDefault();
            userDBData.WebSession = newWebSessionId.Value;
            _database.SaveChanges();
        }

        private static User ToEntity(USER from)
        {
            return new User(
                userId: new UserId(from.Id),
                UserName: new UserName(from.UserName),
                createdBy: new UserId(from.CreatedBy),
                modifiedBy: new UserId(from.ModifiedBy),
                webSessionId: new WebSessionId(from.WebSession),
                created: from.Created,
                modified: from.Modified,
                isDeleted: from.IsDeleted);
        }

        private static IEnumerable<User> ToEntities(IEnumerable<USER> from)
        {
            foreach (var i in from)
            {
                yield return ToEntity(i);
            }
        }

        private static USER ToDBModel(User from) 
        {
            return new USER()
            {
                Id = from.UserId.Value,
                UserName = from.UserName.Value,
                CreatedBy = from.CreatedBy.Value,
                ModifiedBy = from.ModifiedBy.Value,
                WebSession = from.WebSessionId.Value,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted
            };
        }
    }
}
