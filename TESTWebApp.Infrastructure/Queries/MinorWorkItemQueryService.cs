using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.MinorWorkItems.Queries;

namespace TESTWebApp.Infrastructure.Queries
{
    public class MinorWorkItemQueryService : IMinorWorkItemQueryService
    {
        private readonly AppDbContext _database;

        public MinorWorkItemQueryService(AppDbContext database)
        {
            _database = database;
        }

        public IEnumerable<MinorWorkItemDataResponse> GetAllMinorWorkItem(string findKey)
        {
            var responseModels = _database.MinorWorkItemModels
                .Where(x => x.MiddleWorkItemId == findKey)
                .Join(_database.UserDataModels,
                minorWorkItems => minorWorkItems.ModifiedBy,
                users => users.Id,
                (minorWorkItem, user) => new MinorWorkItemDataResponse(
                    minorWorkItem.Id,
                    minorWorkItem.MinorWorkItemName,
                    user.Id,
                    user.UserName,
                    minorWorkItem.Created,
                    minorWorkItem.Modified,
                    minorWorkItem.IsDeleted));

            if (!responseModels.Any())
                return new List<MinorWorkItemDataResponse>();

            return responseModels;
        }


        public MinorWorkItemDataResponse GetMinorWorkItem(string key) 
        {
            var responseModels = _database.MinorWorkItemModels
                .Where(x => x.Id == key)
                .Join(_database.UserDataModels,
                minorWorkItems => minorWorkItems.ModifiedBy,
                users => users.Id,
                (minorWorkItem, modifiedUser) => new { minorWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.minorWorkItem.CreatedBy,
                users => users.Id,
                (minorWorkItem, createUsers) => new { minorWorkItem, createUsers })
                .Select(data => new MinorWorkItemDataResponse(
                    data.minorWorkItem.minorWorkItem.Id,
                    data.minorWorkItem.minorWorkItem.MinorWorkItemName,
                    data.createUsers.UserName,
                    data.minorWorkItem.modifiedUser.UserName,
                    data.minorWorkItem.minorWorkItem.Created,
                    data.minorWorkItem.minorWorkItem.Modified,
                    data.minorWorkItem.minorWorkItem.IsDeleted
                    )).FirstOrDefault();

            return responseModels;
        }
    }
}
