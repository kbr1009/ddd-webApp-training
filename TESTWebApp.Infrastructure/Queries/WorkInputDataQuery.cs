using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.WorkInputs.Queries;

namespace TESTWebApp.Infrastructure.Queries
{
    public sealed class WorkInputDataQuery : IWorkInputDataQuery
    {
        private readonly AppDbContext _database;

        public WorkInputDataQuery(AppDbContext database)
        {
            _database = database;
        }

        public IEnumerable<WorkInputDataResponse> FindAllWorkInputData(DateTime date)
        {
            IEnumerable<WORKINPUT> todoDataModels = _database.WorkInputDataModels
                .Where(x => x.TimeStamp.Date == date.Date);

            if (!todoDataModels.Any())
                return new List<WorkInputDataResponse>();

            return ToModels(todoDataModels);
        }

        public IEnumerable<WorkInputDataResponse> FindAllWorkInputData()
        {
            IEnumerable<WORKINPUT> todoDataModels = _database.WorkInputDataModels
                .Where(x => x.TimeStamp.Date == DateTime.Now.Date);

            if (!todoDataModels.Any())
                return new List<WorkInputDataResponse>();

            return ToModels(todoDataModels);
        }

        public IEnumerable<WorkInputDataResponse> FindAllWorkInputData(string userId)
        {
            IEnumerable<WORKINPUT> todoDataModels = _database.WorkInputDataModels
                .Where(x => x.TimeStamp.Date == DateTime.Now.Date)
                .Where(x => x.UserId == userId);

            if (!todoDataModels.Any())
                return new List<WorkInputDataResponse>();

            return ToModels(todoDataModels);
        }

        private static IEnumerable<WorkInputDataResponse> ToModels(IEnumerable<WORKINPUT> dataModels)
        {
            foreach (var i in dataModels)
                yield return ToModel(i);
        }

        private static WorkInputDataResponse ToModel(WORKINPUT dataModel)
        {
            return new WorkInputDataResponse()
            {
                Id = dataModel.Id,
                UserId = dataModel.UserId,
                WorkItem = dataModel.WorkItem,
                WorkStatus = dataModel.Status,
                TimeStamp = dataModel.TimeStamp,
            };
        }
    }
}
