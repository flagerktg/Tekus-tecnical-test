using Application.Repositories;
using Domain.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CountryRepository : GenericSqlRepository<Country>, ICountryRepository
    {
        private readonly SqlServerDbContext _dbContext;

        public CountryRepository(
            Lazy<SqlServerDbContext> context
        ) : base(context)
        {
            _dbContext = context.Value;
        }

        public IEnumerable<Country> GetCountriesByCodes(IEnumerable<string> countryCodes)
        {
            // Implementar la lógica para obtener los países según sus códigos
            return _dbContext.Countries
                .Where(c => countryCodes.Contains(c.Code))
                .ToList();
        }
    }
}
