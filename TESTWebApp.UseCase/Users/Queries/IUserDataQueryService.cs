
namespace TESTWebApp.UseCase.Users.Queries
{
    public interface IUserDataQueryService
    {
        IEnumerable<UserDataResponse> GetAllUser();
        UserDataResponse FindUserById(string id);
    }
}
