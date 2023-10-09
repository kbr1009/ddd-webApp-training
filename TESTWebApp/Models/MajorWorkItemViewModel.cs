using TESTWebApp.UseCase.MajorWorkItems.Queries;

namespace TESTWebApp.Models
{
    public class MajorWorkItemViewModel
    {
        public string LoginUserName { get; set; }
        public IEnumerable<MajorWorkItemDataResponse> MajorWorkItems { get; set; }
        public string NewMajorWorkItemName { get; set; } = string.Empty;
    }
}
