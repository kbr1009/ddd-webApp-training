using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.Users
{
    public sealed class UserId : Identity
    {
        public UserId(string value) : base(value) { }
        public UserId() { }
    }
}
