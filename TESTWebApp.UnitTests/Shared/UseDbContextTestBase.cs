using Microsoft.EntityFrameworkCore;
using TESTWebApp.Infrastructure.Database;

namespace TESTWebApp.UnitTests.Shared
{
    public abstract class UseDbContextTestBase
    {
        protected AppDbContext _database;

        protected UseDbContextTestBase()
        {
            _database = new AppDbContext(
                options: new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("inMemory_test_db")
                    .Options);

            _database.Database.EnsureCreated();
        }
    }
}
