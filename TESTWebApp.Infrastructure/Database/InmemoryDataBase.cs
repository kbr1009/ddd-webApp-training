using TESTWebApp.Infrastructure.Database.Tables;

namespace TESTWebApp.Infrastructure.Database
{
    public static class InmemoryDataBase
    {
        public static IEnumerable<Dictionary<string, string>> WorkMstList = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>(){ { "1", "えさやり"} },
            new Dictionary<string, string>(){ { "2", "掃除"} },
            new Dictionary<string, string>(){ { "3", "売り出し"} },
            new Dictionary<string, string>(){ { "4", "外出"} }
        };

        public static List<WorkInputDataModel> WorkInputDataModels = new List<WorkInputDataModel>();
    }
}
