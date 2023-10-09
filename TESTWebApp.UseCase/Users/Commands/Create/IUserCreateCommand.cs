
namespace TESTWebApp.UseCase.Users.Commands.Create
{
    public interface IUserCreateCommand
    {
        void Execute(CreateUserRequest request);
    }
}
