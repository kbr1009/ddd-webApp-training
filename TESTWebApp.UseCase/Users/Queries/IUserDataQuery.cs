
namespace TESTWebApp.UseCase.Users.Queries
{
    public interface IUserDataQuery
    {
        IEnumerable<UserDataResponse> GetAllUser();
        UserDataResponse FindUserById(string id);
    }
}
