using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Infrastructure.Database.Tables;

namespace TESTWebApp.Infrastructure.Database
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<WORKITEM> WorkItemModels { get; set; }
        public DbSet<WORKINPUT> WorkInputDataModels { get; set; }
        public DbSet<USER> UserDataModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
