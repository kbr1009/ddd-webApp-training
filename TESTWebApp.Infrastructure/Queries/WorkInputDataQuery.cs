using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.UseCase.WorkInputs.Data;

namespace TESTWebApp.Infrastructure.Queries
{
    public class WorkInputDataQuery : IWorkInputDataQuery
    {
        private readonly AppMoqDbContext _appDbContext;

        public WorkInputDataQuery(AppMoqDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public WorkInputDataQuery()
        {
            this._appDbContext = new AppMoqDbContext();
        }


        public IEnumerable<WorkInputData> FindAllWorkInputData(DateTime date)
        {
            IEnumerable<WorkInputDataModel> todoDataModels = _appDbContext.Datas
                .Where(x => x.TimeStamp.Date == date.Date);

            if (!todoDataModels.Any())
                return new List<WorkInputData>();

            return ToModels(todoDataModels);
        }

        public IEnumerable<WorkInputData> FindAllWorkInputData()
        {
            IEnumerable<WorkInputDataModel> todoDataModels = _appDbContext.Datas
                .Where(x => x.TimeStamp.Date == DateTime.Now.Date);

            if (!todoDataModels.Any())
                return new List<WorkInputData>();

            return ToModels(todoDataModels);
        }

        private static IEnumerable<WorkInputData> ToModels(IEnumerable<WorkInputDataModel> dataModels)
        {
            foreach (var i in dataModels)
                yield return ToModel(i);
        }

        private static WorkInputData ToModel(WorkInputDataModel dataModel)
        {
            return new WorkInputData()
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
