using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Domain.Services.MajorWorkItems;

namespace TESTWebApp.Infrastructure.Repositories
{
    public sealed class MajorWorkItemRepository : IMajorWorkItemRepository
    {
        private readonly AppDbContext _database;

        public MajorWorkItemRepository(AppDbContext database)
        {
            _database = database;
        }

        public MajorWorkItem FindByWorkItemName(string workItemName)
        {
            MAJORWORKITEM dbdata = _database.MajorWorkItemModels
                .Where(x => x.MajorWorkItemName == workItemName)
                .FirstOrDefault();

            if (dbdata == null) return null;

            return ToEntity(dbdata);
        }


        public MajorWorkItem FindByWorkItemId(string workItemId)
        {
            MAJORWORKITEM dbdata = _database.MajorWorkItemModels
                .Where(x => x.Id == workItemId)
                .FirstOrDefault();

            if (dbdata == null) return null;

            return ToEntity(dbdata);
        }


        public void SaveNewWorkItem(MajorWorkItem newWorkItem)
        {
            _database.MajorWorkItemModels.Add(ToDBModel(newWorkItem));
            _database.SaveChanges();
        }


        public void ExecuteUpdateWorkItem(MajorWorkItem from)
        {
            var fromData = ToDBModel(from);
            var majorWorkItemDBData = _database.MajorWorkItemModels
                .Where(x => x.Id == fromData.Id).FirstOrDefault();

            majorWorkItemDBData.MajorWorkItemName = fromData.MajorWorkItemName;
            majorWorkItemDBData.IsDeleted = fromData.IsDeleted;
            majorWorkItemDBData.ModifiedBy = fromData.ModifiedBy;
            majorWorkItemDBData.Modified = fromData.Modified;
            _database.SaveChanges();
        }


        /// <summary>
        /// 大作業項目のIDをキーにデータを削除する
        /// 関連する中作業項目、それに関連する小作業項目もカスケード削除する => できなかったので後回し
        /// </summary>
        public void ExecuteDelWorkItem(MajorWorkItem from)
        {
            var delDatas = _database.MajorWorkItemModels
                .Single(item => item.Id == from.WorkItemId.Value);

            _database.MajorWorkItemModels.Remove(delDatas);
            _database.SaveChanges();
        }


        public int CountsUsedInWorkInputTable(string workItemId)
        {
            int count = _database.WorkInputDataModels
                .Where(x => x.MajorWorkItemId == workItemId)
                .Count();
            return count;
        }


        private static MajorWorkItem ToEntity(MAJORWORKITEM from)
        {
            return new MajorWorkItem(
                workItemId: new MajorWorkItemId(from.Id),
                majorWorkItemName: new WorkItemName(from.MajorWorkItemName),
                createUserId: new UserId(from.CreatedBy),
                modifiedUserId: new UserId(from.ModifiedBy),
                created: from.Created,
                modified: from.Modified,
                isDeleted: from.IsDeleted);
        }


        private static IEnumerable<MajorWorkItem> ToEntities(IEnumerable<MAJORWORKITEM> from)
        {
            foreach (var i in from)
            {
                yield return ToEntity(i);
            }
        }


        private static MAJORWORKITEM ToDBModel(MajorWorkItem from)
        {
            return new MAJORWORKITEM()
            {
                Id = from.WorkItemId.Value,
                MajorWorkItemName = from.MajorWorkItemName.Value,
                CreatedBy = from.CreatedBy.Value,
                ModifiedBy = from.ModifiedBy.Value,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted
            };
        }
    }
}
