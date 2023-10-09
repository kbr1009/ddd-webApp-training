using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.MajorWorkItems.Queries;

namespace TESTWebApp.Infrastructure.Queries
{
    public sealed class MajorWorkItemQueryService : IMajorWorkItemQueryService
    {
        private readonly AppDbContext _database;

        public MajorWorkItemQueryService(AppDbContext database)
        { 
            _database = database;
        }


        public IEnumerable<MajorWorkItemDataResponse> GetAllMajorWorkItem()
        {
            var responseModels = _database.MajorWorkItemModels
                .Join(_database.UserDataModels,
                majorWorkItems => majorWorkItems.ModifiedBy,
                users => users.Id,
                (majorWorkItem, modifiedUser) => new { majorWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.majorWorkItem.CreatedBy,
                users => users.Id,
                (majorWorkItem, createUsers) => new { majorWorkItem, createUsers })
                .Select(data => new MajorWorkItemDataResponse(
                    data.majorWorkItem.majorWorkItem.Id,
                    data.majorWorkItem.majorWorkItem.MajorWorkItemName,
                    data.createUsers.UserName,
                    data.majorWorkItem.modifiedUser.UserName,
                    data.majorWorkItem.majorWorkItem.Created,
                    data.majorWorkItem.majorWorkItem.Modified,
                    data.majorWorkItem.majorWorkItem.IsDeleted
                    ));

            if (!responseModels.Any())
                return new List<MajorWorkItemDataResponse>();

            return responseModels;
        }


        /// <summary>
        /// 論理削除アイテムをのぞく
        /// </summary>
        public IEnumerable<MajorWorkItemDataResponse> GetMajorWorkItemsNotDel()
        {
            var responseModels = _database.MajorWorkItemModels
                // 論理削除されているものは除く
                .Where(d => d.IsDeleted == false)
                .Join(_database.UserDataModels,
                majorWorkItems => majorWorkItems.ModifiedBy,
                users => users.Id,
                (majorWorkItem, modifiedUser) => new { majorWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.majorWorkItem.CreatedBy,
                users => users.Id,
                (majorWorkItem, createUsers) => new { majorWorkItem, createUsers })
                .Select(data => new MajorWorkItemDataResponse(
                    data.majorWorkItem.majorWorkItem.Id,
                    data.majorWorkItem.majorWorkItem.MajorWorkItemName,
                    data.createUsers.UserName,
                    data.majorWorkItem.modifiedUser.UserName,
                    data.majorWorkItem.majorWorkItem.Created,
                    data.majorWorkItem.majorWorkItem.Modified,
                    data.majorWorkItem.majorWorkItem.IsDeleted
                    ));

            if (!responseModels.Any())
                return new List<MajorWorkItemDataResponse>();

            return responseModels;
        }


        public MajorWorkItemDataResponse GetMajorWorkItem(string id)
        {
            var responseModels = _database.MajorWorkItemModels
                .Where(x => x.Id == id)
                .Join(_database.UserDataModels,
                majorWorkItems => majorWorkItems.ModifiedBy,
                users => users.Id,
                (majorWorkItem, modifiedUser) => new { majorWorkItem, modifiedUser })
                .Join(_database.UserDataModels,
                x => x.majorWorkItem.CreatedBy,
                users => users.Id,
                (majorWorkItem, createUsers) => new { majorWorkItem, createUsers})
                .Select(data => new MajorWorkItemDataResponse(
                    data.majorWorkItem.majorWorkItem.Id,
                    data.majorWorkItem.majorWorkItem.MajorWorkItemName,
                    data.createUsers.UserName,
                    data.majorWorkItem.modifiedUser.UserName,
                    data.majorWorkItem.majorWorkItem.Created,
                    data.majorWorkItem.majorWorkItem.Modified,
                    data.majorWorkItem.majorWorkItem.IsDeleted
                    )).FirstOrDefault();

            return responseModels;
        }
    }
}
