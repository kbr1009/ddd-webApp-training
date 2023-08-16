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

        public void SaveNewUser(User newUser)
        {
            _database.UserDataModels.Add(ToDBModel(newUser));
            _database.SaveChanges();
        }

        private static User ToEntity(USER from)
        {
            return new User(
                userId: new UserId(from.Id),
                UserName: from.UserName,
                createdBy: new UserId(from.CreatedBy),
                modifiedBy: new UserId(from.ModifiedBy),
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
                UserName = from.UserName,
                CreatedBy = from.CreatedBy.Value,
                ModifiedBy = from.ModifiedBy.Value,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted
            };
        }
    }
}
