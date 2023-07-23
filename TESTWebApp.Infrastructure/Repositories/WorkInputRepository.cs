using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.Infrastructure.Database;
using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.Infrastructure.Repositories
{
    public class WorkInputRepository : IWorkInputRepository
    {
        private readonly AppMoqDbContext _appDbContext;

        public WorkInputRepository(AppMoqDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public WorkInputRepository()
        {
            this._appDbContext = new AppMoqDbContext();
        }


        public void RegistWorkInput(WorkInput workInput)
        {
            try { _appDbContext.AddData(ToDataModel(workInput)); }
            catch { throw new ArgumentException("error!: データの登録に失敗しました。"); }
        }

        public IEnumerable<WorkInput> FindAllToModel(DateTime date)
        {
            IEnumerable<WorkInputDataModel> todoDataModels = _appDbContext.Datas
                .Where(x => x.TimeStamp.Date == date.Date);

            if (!todoDataModels.Any())
                return new List<WorkInput>();

            return ToModels(todoDataModels);
        }

        public WorkInput FindById(string id)
        {
            WorkInputDataModel? workInput = _appDbContext.Datas
                .FirstOrDefault(x => x.Id == id);
            if (workInput is null)
                throw new Exception($"error!: {id} の履歴がみつかりません。");
            return ToModel(workInput);
        }

        private static IEnumerable<WorkInput> ToModels(IEnumerable<WorkInputDataModel> dataModels)
        {
            foreach (var i in dataModels)
                yield return ToModel(i);
        }

        private static WorkInput ToModel(WorkInputDataModel dataModel)
        {
            return new WorkInput(
                    id: new WorkInputId(dataModel.Id),
                    userId: dataModel.UserId,
                    workItem: dataModel.WorkItem,
                    status: (WorkStatus)dataModel.Status,
                    timeStamp: dataModel.TimeStamp,
                    isDeleted: dataModel.IsDeleted);
        }

        private static WorkInputDataModel ToDataModel(WorkInput workInput)
        {
            return new WorkInputDataModel()
            {
                Id = workInput.Id.Value,
                UserId = workInput.UserId,
                WorkItem = workInput.WorkItem,
                Status = (int)workInput.Status,
                TimeStamp = workInput.TimeStamp,
                IsDeleted = workInput.IsDeleted
            };
        }
    }
}
