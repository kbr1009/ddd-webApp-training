using TESTWebApp.Domain.Services.MinorWorkItems;
using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Infrastructure.Database.Tables;

namespace TESTWebApp.Infrastructure.Repositories
{
    public class MinorWorkItemRepository : IMinorWorkItemRepository
    {
        private readonly AppDbContext _database;

        public MinorWorkItemRepository(AppDbContext database)
        {
            _database = database;
        }

        public MinorWorkItem FindByWorkItemName(string foreignKey, string workItemName)
        {
            var responseModels = _database.MinorWorkItemModels
                .Where(x => x.MinorWorkItemName == workItemName)
                .Where(X => X.MiddleWorkItemId == foreignKey)
                //.Where(x => x.IsDeleted == false)
                .Select(x => new MinorWorkItem(
                    new MinorWorkItemId(x.Id),
                    new WorkItemName(x.MinorWorkItemName),
                    new MajorWorkItemId(x.MajorWorkItemId),
                    new MiddleWorkItemId(x.MiddleWorkItemId),
                    new UserId(x.CreatedBy),
                    new UserId(x.ModifiedBy),
                    x.Created,
                    x.Modified,
                    false))
                .FirstOrDefault();
            return responseModels;
        }


        public MinorWorkItem FindById(string id)
        {
            var dbdata = _database.MinorWorkItemModels
                .Where(x => x.Id == id)
                .Select(x => new MinorWorkItem(
                    new MinorWorkItemId(x.Id),
                    new WorkItemName(x.MinorWorkItemName),
                    new MajorWorkItemId(x.MajorWorkItemId),
                    new MiddleWorkItemId(x.MiddleWorkItemId),
                    new UserId(x.CreatedBy),
                    new UserId(x.ModifiedBy),
                    x.Created,
                    x.Modified,
                    x.IsDeleted))
                .FirstOrDefault();

            return dbdata;
        }


        public void SaveNewWorkItem(MinorWorkItem from)
        {
            _database.MinorWorkItemModels.Add(
                new MINORWORKITEM
                {
                    Id = from.MinorWorkItemId.Value,
                    MinorWorkItemName = from.MinorWorkItemName.Value,
                    MajorWorkItemId = from.MajorWorkItemId.Value,
                    MiddleWorkItemId = from.MajorWorkItemId.Value,
                    CreatedBy = from.CreatedBy.Value,
                    ModifiedBy = from.ModifiedBy.Value,
                    Created = from.Created,
                    Modified = from.Modified,
                    IsDeleted = from.IsDeleted,
                });
            _database.SaveChanges();
        }


        public void ExecuteUpdateWorkItem(MinorWorkItem from)
        {
            var fromData = new MINORWORKITEM
            {
                Id = from.MinorWorkItemId.Value,
                MinorWorkItemName = from.MinorWorkItemName.Value,
                MajorWorkItemId = from.MajorWorkItemId.Value,
                MiddleWorkItemId= from.MiddleWorkItemId.Value,
                CreatedBy = from.CreatedBy.Value,
                ModifiedBy = from.ModifiedBy.Value,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted,
            };
            var minorWorkItemDBData = _database.MinorWorkItemModels
                .Where(x => x.Id == fromData.Id)
                .FirstOrDefault();

            minorWorkItemDBData.MinorWorkItemName = fromData.MinorWorkItemName;
            minorWorkItemDBData.IsDeleted = fromData.IsDeleted;
            minorWorkItemDBData.ModifiedBy = fromData.ModifiedBy;
            minorWorkItemDBData.Modified = fromData.Modified;
            _database.SaveChanges();
        }


        public void ExecuteDelWorkItem(MinorWorkItem from)
        {
            var delDatas = _database.MinorWorkItemModels
                .Single(item => item.Id == from.MinorWorkItemId.Value);

            _database.MinorWorkItemModels.Remove(delDatas);
            _database.SaveChanges();
        }

        public int CountsUsedInWorkInputTable(string minorWorkItemId)
        {
            int count = _database.WorkInputDataModels
                .Where(x => x.MinorWorkItemId == minorWorkItemId)
                .Count();
            return count;
        }
    }
}
