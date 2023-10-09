using TESTWebApp.UseCase.MajorWorkItems.Queries;

namespace TESTWebApp.Models.MajorWorkItem
{
    public class MajorWorkItemEditViewModel
    {
        public string LoginUserName { get; set; }
        public MajorWorkItemDataResponse CurrentMajorWorkItem { get; set; }
        public string EditMajorWorkItemId { get; set; } = string.Empty;
        public string NewMajorWorkItemName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
