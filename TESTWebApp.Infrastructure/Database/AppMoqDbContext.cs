using TESTWebApp.Infrastructure.Database.Tables;

namespace TESTWebApp.Infrastructure.Database
{
    public class AppMoqDbContext
    {
        public readonly IEnumerable<WorkInputDataModel> Datas = InmemoryDataBase.WorkInputDataModels;

        public void AddData(WorkInputDataModel workInputDataModel)
        {
            InmemoryDataBase.WorkInputDataModels.Add(workInputDataModel);
        }
    }
}
