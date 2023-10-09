using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.MinorWorkItems
{
    public sealed class MinorWorkItemId : Identity
    {
        public MinorWorkItemId(string value) : base(value) { }
        public MinorWorkItemId() { }
    }
}
