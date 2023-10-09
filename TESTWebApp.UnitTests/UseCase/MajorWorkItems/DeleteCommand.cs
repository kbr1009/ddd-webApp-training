using TESTWebApp.Domain.Services.MajorWorkItems;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.Infrastructure.Database.Tables;
using TESTWebApp.UnitTests.Shared;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Delete;

namespace TESTWebApp.UnitTests.UseCase.MajorWorkItems
{
    [TestClass]
    public class DeleteCommand : UseDbContextTestBase
    {
        [TestMethod]
        public void 作業項目大が正しく削除できる()
        {
            string createdBy = Guid.NewGuid().ToString("N");
            DateTime now = DateTime.Now;
            var moqData = new MAJORWORKITEM()
            {
                Id = Guid.NewGuid().ToString("N"),
                MajorWorkItemName = "TESTItem",
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Created = now,
                Modified = now,
                IsDeleted = false,
            };
            base._database.MajorWorkItemModels.Add(moqData);
            IMajorWorkItemRepository repository = new MajorWorkItemRepository(base._database);

            string deletedBy = Guid.NewGuid().ToString("N");
            DeleteMajorWorkItemRequest testData = new DeleteMajorWorkItemRequest(moqData.Id, deletedBy);

            bool ok = true;
            try
            {
                IMajorWorkItemDeleteCommand command = new MajorWorkItemDeleteCommand(repository);
                command.Execute(testData);
            }
            catch (Exception ex)
            {
                ok = false;
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void 作業項目大が正しく削除できない()
        {
            string createdBy = Guid.NewGuid().ToString("N");
            DateTime now = DateTime.Now;
            var moqData = new MAJORWORKITEM()
            {
                Id = Guid.NewGuid().ToString("N"),
                MajorWorkItemName = "TESTItem",
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Created = now,
                Modified = now,
                IsDeleted = false,
            };
            base._database.MajorWorkItemModels.Add(moqData);
            IMajorWorkItemRepository repository = new MajorWorkItemRepository(base._database);

            string deletedBy = Guid.NewGuid().ToString("N");
            DeleteMajorWorkItemRequest testData = new DeleteMajorWorkItemRequest(Guid.NewGuid().ToString("N"), deletedBy);

            bool fail = true;
            try
            {
                IMajorWorkItemDeleteCommand command = new MajorWorkItemDeleteCommand(repository);
                command.Execute(testData);
                fail = false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            Assert.IsTrue(fail);
        }
    }
}
