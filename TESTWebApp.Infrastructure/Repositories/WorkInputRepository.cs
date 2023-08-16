using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.Domain.Services.WorkInputs;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Infrastructure.Repositories
{
    public sealed class WorkInputRepository : IWorkInputRepository
    {
        private readonly AppDbContext _database;

        public WorkInputRepository(AppDbContext database)
        {
            _database = database;
        }

        public void RegistWorkInput(WorkInput workInput)
        {
            _database.Add(ToDataModel(workInput));
            _database.SaveChanges();
        }

        private static IEnumerable<WorkInput> ToModels(IEnumerable<WORKINPUT> dataModels)
        {
            foreach (var i in dataModels)
                yield return ToModel(i);
        }

        private static WorkInput ToModel(WORKINPUT dataModel)
        {
            return new WorkInput(
                    id: new WorkInputId(dataModel.Id),
                    userId: new UserId( dataModel.UserId),
                    workItem: dataModel.WorkItem,
                    status: (WorkStatus)dataModel.Status,
                    timeStamp: dataModel.TimeStamp,
                    isDeleted: dataModel.IsDeleted);
        }

        private static WORKINPUT ToDataModel(WorkInput workInput)
        {
            return new WORKINPUT()
            {
                Id = workInput.Id.Value,
                UserId = workInput.UserId.Value,
                WorkItem = workInput.WorkItem,
                Status = (int)workInput.Status,
                TimeStamp = workInput.TimeStamp,
                IsDeleted = workInput.IsDeleted
            };
        }
    }
}
