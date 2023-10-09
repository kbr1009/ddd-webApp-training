using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.MajorWorkItems
{
    public class MajorWorkItemId : Identity
    {
        public MajorWorkItemId(string value) : base(value) { }
        public MajorWorkItemId() { }
    }
}
