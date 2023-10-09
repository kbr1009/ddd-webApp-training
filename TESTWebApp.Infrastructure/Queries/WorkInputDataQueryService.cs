using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.WorkInputs.Queries;

namespace TESTWebApp.Infrastructure.Queries
{
    public sealed class WorkInputDataQueryService : IWorkInputDataQueryService
    {
        private readonly AppDbContext _database;

        public WorkInputDataQueryService(AppDbContext database)
        {
            _database = database;
        }

        public IEnumerable<WorkInputDataResponse> FindWorkInputDataForToday(string userId, DateTime today)
        {
            // 参考：
            //   https://qiita.com/kyv28v/items/8d41a2aa8fb01d3c329b
            var todoDataModels = _database.WorkInputDataModels
                .Join(_database.MajorWorkItemModels,
                a => a.MajorWorkItemId,
                b => b.Id,
                (a, b) => new { a, b })
                .GroupJoin(_database.MiddleWorkItemModels,
                ab => ab.a.MiddleWorkItemId,
                c => c.Id,
                (ab, c) => new { ab, c })
                .SelectMany(c => c.c.DefaultIfEmpty(),
                (ab, c) => new { ab, c })
                .GroupJoin(_database.MinorWorkItemModels,
                abc => abc.ab.ab.a.MinorWorkItemId,
                d => d.Id,
                (abc, d) => new { abc, d })
                .SelectMany(d => d.d.DefaultIfEmpty(),
                (abc, d) => new { abc, d })
                // 今日の日付
                .Where(x => x.abc.abc.ab.ab.a.TimeStamp.Date == today.Date)
                // 指定したユーザID
                .Where(x => x.abc.abc.ab.ab.a.UserId == userId)
                // 新しい順番
                .OrderBy(x => x.abc.abc.ab.ab.a.TimeStamp)
                .Select(y => new WorkInputDataResponse
                {
                    Id = y.abc.abc.ab.ab.a.Id,
                    UserId = y.abc.abc.ab.ab.a.UserId,
                    MajorWorkItemId = y.abc.abc.ab.ab.b.Id,
                    MajorWorkItemName = y.abc.abc.ab.ab.b.MajorWorkItemName,
                    MiddleWorkItemId = y.abc.abc.c.Id,
                    MiddleWorkItemName = y.abc.abc.c.MiddleWorkItemName,
                    MinorWorkItemId = y.d.Id,
                    MinorWorkItemName = y.d.MinorWorkItemName,
                    WorkStatus = y.abc.abc.ab.ab.a.Status,
                    TimeStamp = y.abc.abc.ab.ab.a.TimeStamp
                });

            if (!todoDataModels.Any())
                return new List<WorkInputDataResponse>();

            return todoDataModels;
        }

        public WorkInputDataResponse GetCurrentWork(string userId, DateTime today)
        {
            return new WorkInputDataResponse();
        }
    }
}
