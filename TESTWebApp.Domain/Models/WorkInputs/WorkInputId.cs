using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.WorkInputs
{
    public sealed class WorkInputId : SingleValueObject<string>
    {
        public WorkInputId(string val) : base(val) { }

        public static WorkInputId Generate()
        {
            return new WorkInputId(Guid.NewGuid().ToString("N"));
        }
    }
}
