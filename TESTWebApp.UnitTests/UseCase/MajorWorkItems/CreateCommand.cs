using TESTWebApp.Domain.Services.MajorWorkItems;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.UnitTests.Shared;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Create;

namespace TESTWebApp.UnitTests.UseCase.MajorWorkItems
{
    [TestClass]
    public class CreateCommand : UseDbContextTestBase
    {
        private readonly IMajorWorkItemRepository _repository;

        public CreateCommand()
        {
            _repository = new MajorWorkItemRepository(base._database);
        }

        [TestMethod]
        public void 作業項目が正しく登録できる()
        {
            CreateMajorWorkItemRequest testData = 
                new CreateMajorWorkItemRequest("清掃", "test-test-test-test");

            bool ok = true;
            try
            {
                IMajorWorkItemCreateCommand command = new MajorWorkItemCreateCommand(_repository);
                command.Execute(testData);
            }
            catch (Exception ex)
            {
                ok = false;
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            Assert.IsTrue(ok);
        }
    }
}
