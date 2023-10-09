using System.ComponentModel.DataAnnotations;
using TESTWebApp.UseCase.MinorWorkItems.Queries;

namespace TESTWebApp.Models
{
    public class MinorWorkItemViewModel
    {
        public string LoginUserName { get; set; }
        public IEnumerable<MinorWorkItemDataResponse> MinorWorkItems { get; set; }
        public string MajorWorkItemId { get; set; } = string.Empty;
        public string MajorWorkItemName { get; set; } = string.Empty;
        public string MiddleWorkItemId { get; set; } = string.Empty;
        public string MiddleWorkItemName { get; set; } = string.Empty;

        [Required(ErrorMessage ="入力は必須です。")]
        public string NewMinorWorkItemName { get; set; } = string.Empty;
    }
}
