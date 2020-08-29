using Microsoft.EntityFrameworkCore;
using Model.Domain.Entities;
using Model.Infra.Data.Mapping;

namespace Model.Infra.Data.Context
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {            
        }

        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMapping());
        }
    }
}
