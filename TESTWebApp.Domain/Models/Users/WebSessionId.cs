using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.Users
{
    public class WebSessionId : Identity
    {
        public WebSessionId(string value) : base(value) { }
        public WebSessionId() { }
    }
}
