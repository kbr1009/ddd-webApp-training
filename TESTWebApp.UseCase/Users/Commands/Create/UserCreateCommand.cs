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
                userName: new UserName(request.UserName),
                createdBy: new UserId(request.CreatedBy));

            UserService userService = new(_repository);
            if (userService.IsDupulicatedUser(newUser))
                throw new ArgumentException($"'{request.UserName}' はすでに登録されています。");

            _repository.SaveNewUser(newUser);
        }
    }
}
