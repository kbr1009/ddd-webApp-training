using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.MajorWorkItems
{
    public class WorkItemName : SingleValueObject<string>
    {
        public WorkItemName(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("作業項目の入力は必須です。");

            if (value.Contains(' ') || value.Contains('　'))
                throw new ArgumentException("登録する作業項目にスペースを含めることはできません。");
        }

        public override bool Equals(object obj)
        {
            var identity = obj as WorkItemName;
            return identity != null &&
                   Value == identity.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
