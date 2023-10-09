using Microsoft.EntityFrameworkCore;
using TESTWebApp.Infrastructure.Database.Tables;

namespace TESTWebApp.Infrastructure.Database
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<MAJORWORKITEM> MajorWorkItemModels { get; set; }
        public DbSet<MIDDLEWORKITEM> MiddleWorkItemModels { get; set; }
        public DbSet<MINORWORKITEM> MinorWorkItemModels { get; set; }
        public DbSet<WORKINPUT> WorkInputDataModels { get; set; }
        public DbSet<USER> UserDataModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
