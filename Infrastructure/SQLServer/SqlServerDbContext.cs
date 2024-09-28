using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.SQLServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            #region Relación muchos a muchos entre Service y Country

            builder.Entity<Service>()
                .HasMany(left => left.Countries)
                .WithMany(right => right.Services)
                .UsingEntity(join => join.ToTable("CountryServices"));

            #endregion
        }
    }
}
