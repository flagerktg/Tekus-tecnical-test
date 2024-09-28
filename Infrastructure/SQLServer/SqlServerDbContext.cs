using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.SQLServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
      

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
