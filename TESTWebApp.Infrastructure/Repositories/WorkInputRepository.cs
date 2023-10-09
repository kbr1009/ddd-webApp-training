using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.Domain.Services.WorkInputs;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.MinorWorkItems;

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
                    userId: new UserId(dataModel.UserId),
                    majorWorkItemId: new MajorWorkItemId(dataModel.MajorWorkItemId),
                    middleWorkItemId: new MiddleWorkItemId(dataModel.MiddleWorkItemId),
                    minorWorkItemId: new MinorWorkItemId(dataModel.MinorWorkItemId),
                    status: (WorkStatus)dataModel.Status,
                    timeStamp: dataModel.TimeStamp);
        }

        private static WORKINPUT ToDataModel(WorkInput workInput)
        {
            return new WORKINPUT()
            {
                Id = workInput.Id.Value,
                UserId = workInput.UserId.Value,
                MajorWorkItemId = workInput.MajorWorkItemId.Value,
                MiddleWorkItemId = workInput.MiddleWorkItemId.Value,
                MinorWorkItemId = workInput.MinorWorkItemId.Value,
                Status = (int)workInput.Status,
                TimeStamp = workInput.TimeStamp
            };
        }
    }
}
