using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.Users;

namespace TESTWebApp.UseCase.Users.Commands.Create
{
    public sealed class UserCreateCommand : IUserCreateCommand
    {
        private readonly IUserRepository _repository;

        public UserCreateCommand(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateUserRequest request)
        {
            User newUser = User.CreateNew(
                userName: request.UserName,
                createdBy: new UserId(request.CreatedBy));

            UserService userService = new UserService(_repository);
            if (userService.IsDupulicatedUser(newUser))
                throw new ArgumentException($"'{request.UserName}' はすでに登録されています。");

            _repository.SaveNewUser(newUser);
        }
    }
}
