using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Services.Users;
using TESTWebApp.Domain.Services.WorkItems;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.UnitTests.Shared;
using TESTWebApp.UseCase.Users.Commands.Create;
using TESTWebApp.UseCase.WorkItems.Commands.Create;

namespace TESTWebApp.UnitTests.UseCase.WorkItems
{
    [TestClass]
    public class CreateCommand : UseDbContextTestBase
    {
        private readonly IWorkItemRepository _repository;

        public CreateCommand()
        {
            _repository = new WorkItemRepository(_database);
        }

        [TestMethod]
        public void 作業項目が正しく登録できる()
        {
            CreateWorkItemRequest testData = new CreateWorkItemRequest("清掃", "test-test-test-test");
            IWorkItemCreateCommand command = new WorkItemCreateCommand(_repository);
            command.Execute(testData);
        }
    }
}
