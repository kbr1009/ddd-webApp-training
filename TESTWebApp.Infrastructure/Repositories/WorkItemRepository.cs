using TESTWebApp.Domain.Services.WorkItems;
using TESTWebApp.Domain.Models.WorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;

namespace TESTWebApp.Infrastructure.Repositories
{
    public sealed class WorkItemRepository : IWorkItemRepository
    {
        private readonly AppDbContext _database;

        public WorkItemRepository(AppDbContext database)
        {
            _database = database;
        }

        public WorkItem FindByWorkItemName(string workItemName)
        {
            WORKITEM dbdata = _database.WorkItemModels
                .Where(x => x.WorkItemName == workItemName)
                .FirstOrDefault();

            if (dbdata == null) return null;

            return ToEntity(dbdata);
        }

        public void SaveNewWorkItem(WorkItem newWorkItem)
        {
            _database.WorkItemModels.Add(ToDBModel(newWorkItem));
            _database.SaveChanges();
        }

        private static WorkItem ToEntity(WORKITEM from)
        {
            return new WorkItem(
                workItemId: new WorkItemId(from.Id),
                workItemName: from.WorkItemName,
                createUserId: new UserId(from.CreatedBy),
                modifiedUserId: new UserId(from.ModifiedBy),
                created: from.Created,
                modified: from.Modified,
                isDeleted: from.IsDeleted);
        }

        private static IEnumerable<WorkItem> ToEntities(IEnumerable<WORKITEM> from)
        {
            foreach (var i in from)
            {
                yield return ToEntity(i);
            }
        }

        private static WORKITEM ToDBModel(WorkItem from)
        {
            return new WORKITEM()
            {
                Id = from.WorkItemId.Value,
                WorkItemName = from.WorkItemName,
                CreatedBy = from.CreatedBy.Value,
                ModifiedBy = from.ModifiedBy.Value,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted
            };
        }
    }
}
