using TESTWebApp.Domain.Services.Users;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.UnitTests.Shared;
using TESTWebApp.UseCase.Users.Commands.Create;

namespace TESTWebApp.UnitTests.UseCase.Users
{
    [TestClass]
    public class CreateCommand : UseDbContextTestBase
    {
        private readonly IUserRepository _repository;

        public CreateCommand()
        {
            _repository = new UserRepository(_database);
        }

        [TestMethod]
        public void ユーザが正しく登録できる()
        {
            var testData = new CreateUserRequest("小林怜雄", "1234-1234-1234");
            IUserCreateCommand userCreateCommand = new UserCreateCommand(_repository);
            userCreateCommand.Execute(testData);
        }
    }
}
