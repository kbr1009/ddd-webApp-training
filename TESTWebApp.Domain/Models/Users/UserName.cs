using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.Users
{
    public sealed class UserName : SingleValueObject<string>
    {
        public UserName(string val) : base(val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentException("作業者名は必須入力項目です。");

            if (val.Contains(' ') || val.Contains('　'))
                throw new ArgumentException("登録する作業者名にスペースを含めることはできません。");
        }
    }
}
