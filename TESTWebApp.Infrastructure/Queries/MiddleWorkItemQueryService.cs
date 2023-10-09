using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.MiddleWorkItems.Queries;

namespace TESTWebApp.Infrastructure.Queries
{
    public class MiddleWorkItemQueryService : IMiddleWorkItemQueryService
    {
        private readonly AppDbContext _database;

        public MiddleWorkItemQueryService(AppDbContext database)
        {
            _database = database;
        }

        public IEnumerable<MiddleWorkItemDataResponse> GetAllMiddleWorkItem(string majorWorkItemId)
        {
            var responseModels = _database.MiddleWorkItemModels
                .Where(x => x.MajorWorkItemId == majorWorkItemId)
                .Join(_database.UserDataModels,
                middleWorkItems => middleWorkItems.ModifiedBy,
                users => users.Id,
                (middleWorkItem, modifiedUser) => new { middleWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.middleWorkItem.CreatedBy,
                users => users.Id,
                (middleWorkItem, createUsers) => new { middleWorkItem, createUsers })
                .Select(data => new MiddleWorkItemDataResponse(
                    data.middleWorkItem.middleWorkItem.Id,
                    data.middleWorkItem.middleWorkItem.MiddleWorkItemName,
                    data.createUsers.UserName,
                    data.middleWorkItem.modifiedUser.UserName,
                    data.middleWorkItem.middleWorkItem.Created,
                    data.middleWorkItem.middleWorkItem.Modified,
                    data.middleWorkItem.middleWorkItem.IsDeleted
                    ));

            if (!responseModels.Any())
                return new List<MiddleWorkItemDataResponse>();

            return responseModels;
        }


        public IEnumerable<MiddleWorkItemDataResponse> GetMajorWorkItemsNotDel(string majorWorkItemId)
        {
            var responseModels = _database.MiddleWorkItemModels
                .Where(x => x.MajorWorkItemId == majorWorkItemId)
                // 論理削除されているものは除く
                .Where(d => d.IsDeleted == false)
                .Join(_database.UserDataModels,
                middleWorkItems => middleWorkItems.ModifiedBy,
                users => users.Id,
                (middleWorkItem, modifiedUser) => new { middleWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.middleWorkItem.CreatedBy,
                users => users.Id,
                (middleWorkItem, createUsers) => new { middleWorkItem, createUsers })
                .Select(data => new MiddleWorkItemDataResponse(
                    data.middleWorkItem.middleWorkItem.Id,
                    data.middleWorkItem.middleWorkItem.MiddleWorkItemName,
                    data.createUsers.UserName,
                    data.middleWorkItem.modifiedUser.UserName,
                    data.middleWorkItem.middleWorkItem.Created,
                    data.middleWorkItem.middleWorkItem.Modified,
                    data.middleWorkItem.middleWorkItem.IsDeleted
                    ));

            if (!responseModels.Any())
                return new List<MiddleWorkItemDataResponse>();

            return responseModels;
        }


        public MiddleWorkItemDataResponse GetMiddleWorkItem(string middleWorkItemId)
        {
            var responseModels = _database.MiddleWorkItemModels
                .Where(x => x.Id == middleWorkItemId)
                .Join(_database.UserDataModels,
                middleWorkItems => middleWorkItems.ModifiedBy,
                users => users.Id,
                (middleWorkItem, modifiedUser) => new { middleWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.middleWorkItem.CreatedBy,
                users => users.Id,
                (middleWorkItem, createUsers) => new { middleWorkItem, createUsers })
                .Select(data => new MiddleWorkItemDataResponse(
                    data.middleWorkItem.middleWorkItem.Id,
                    data.middleWorkItem.middleWorkItem.MiddleWorkItemName,
                    data.createUsers.UserName,
                    data.middleWorkItem.modifiedUser.UserName,
                    data.middleWorkItem.middleWorkItem.Created,
                    data.middleWorkItem.middleWorkItem.Modified,
                    data.middleWorkItem.middleWorkItem.IsDeleted
                    )).FirstOrDefault();

            return responseModels;
        }
    }
}
