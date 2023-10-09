using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Services.Auth
{
    public interface IAuthService
    {
        string UserIdByCookie { get; }
        public UserDataResponse UserData { get; }
        bool Authorize();
    }
}
