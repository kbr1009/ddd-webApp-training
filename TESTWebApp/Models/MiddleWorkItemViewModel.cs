using TESTWebApp.UseCase.MiddleWorkItems.Queries;

namespace TESTWebApp.Models
{
    public class MiddleWorkItemViewModel
    {
        public string LoginUserName { get; set; }
        public IEnumerable<MiddleWorkItemDataResponse> MiddleWorkItems { get; set; }
        public string MajorWorkItemId { get; set; } = string.Empty;
        public string MajorWorkItemName { get; set; } = string.Empty;
        public string NewMiddleWorkItemName { get; set; } = string.Empty;
    }
}
