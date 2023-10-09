using TESTWebApp.Domain.Services.MiddleWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Infrastructure.Database.Tables;


namespace TESTWebApp.Infrastructure.Repositories
{
    public class MiddleWorkItemRepository : IMiddleWorkItemRepository
    {
        private readonly AppDbContext _database;

        public MiddleWorkItemRepository(AppDbContext database)
        {
            _database = database;
        }

        public MiddleWorkItem FindByWorkItemName(string foreignKey, string workItemName)
        {
            var responseModels = _database.MiddleWorkItemModels
                .Where(x => x.MiddleWorkItemName == workItemName)
                .Where(X => X.MajorWorkItemId == foreignKey)
                //.Where(x => x.IsDeleted == false)
                .Select(x => new MiddleWorkItem(
                    new MiddleWorkItemId(x.Id),
                    new MajorWorkItemId(x.MajorWorkItemId),
                    new WorkItemName(x.MiddleWorkItemName),
                    new UserId(x.CreatedBy),
                    new UserId(x.ModifiedBy),
                    x.Created,
                    x.Modified,
                    x.IsDeleted))
                .FirstOrDefault();

            if (responseModels is null)
                return null;

            return responseModels;
        }


        public MiddleWorkItem FindById(string id)
        {
            var dbdata = _database.MiddleWorkItemModels
                .Where(x => x.Id == id)
                .Select(x => new MiddleWorkItem(
                    new MiddleWorkItemId(x.Id),
                    new MajorWorkItemId(x.MajorWorkItemId),
                    new WorkItemName(x.MiddleWorkItemName),
                    new UserId(x.CreatedBy),
                    new UserId(x.ModifiedBy),
                    x.Created,
                    x.Modified,
                    x.IsDeleted))
                .FirstOrDefault();

            return dbdata;
        }


        public void SaveNewWorkItem(MiddleWorkItem from)
        {
            _database.MiddleWorkItemModels.Add(
                new MIDDLEWORKITEM
                { 
                    Id = from.MiddleWorkItemId.Value,
                    MiddleWorkItemName = from.MiddleWorkItemName.Value,
                    MajorWorkItemId = from.MajorWorkItemId.Value,
                    CreatedBy = from.CreatedBy.Value,
                    ModifiedBy = from.ModifiedBy.Value,
                    Created = from.Created,
                    Modified = from.Modified,
                    IsDeleted = from.IsDeleted,
                });
            _database.SaveChanges();
        }


        public void ExecuteUpdateWorkItem(MiddleWorkItem from)
        {
            var fromData = new MIDDLEWORKITEM
            {
                Id = from.MiddleWorkItemId.Value,
                MiddleWorkItemName = from.MiddleWorkItemName.Value,
                MajorWorkItemId = from.MajorWorkItemId.Value,
                CreatedBy = from.CreatedBy.Value,
                ModifiedBy = from.ModifiedBy.Value,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted,
            };
            var middleWorkItemDBData = _database.MiddleWorkItemModels
                .Where(x => x.Id == fromData.Id).FirstOrDefault();

            middleWorkItemDBData.MiddleWorkItemName = fromData.MiddleWorkItemName;
            middleWorkItemDBData.IsDeleted = fromData.IsDeleted;
            middleWorkItemDBData.ModifiedBy = fromData.ModifiedBy;
            middleWorkItemDBData.Modified = fromData.Modified;
            _database.SaveChanges();
        }


        public void ExecuteDelWorkItem(MiddleWorkItem from)
        {
            var delDatas = _database.MiddleWorkItemModels
                .Single(item => item.Id == from.MiddleWorkItemId.Value);

            _database.MiddleWorkItemModels.Remove(delDatas);
            _database.SaveChanges();
        }


        public int CountsUsedInWorkInputTable(string id)
        {
            int count = _database.WorkInputDataModels
                .Where(x => x.MiddleWorkItemId == id)
                .Count();
            return count;
        }
    }
}
