using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar el tipo de columna para PriceByHour con precisión de 18 y escala de 2 (18 dígitos, 2 decimales)
            modelBuilder.Entity<Service>()
                .Property(s => s.PriceByHour)
                .HasColumnType("decimal(18,2)");

            #region Relación muchos a muchos entre Service y Country

            modelBuilder.Entity<Service>()
                .HasMany(left => left.Countries)
                .WithMany(right => right.Services)
                .UsingEntity(join => join.ToTable("CountryServices"));

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
