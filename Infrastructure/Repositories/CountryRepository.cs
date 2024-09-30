using Application.Repositories;
using Domain.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;

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
            return [.. _dbContext.Countries.Where(c => countryCodes.Contains(c.Code))];
        }

        public Country? GetCountryByCode(string code)
        {
            return _dbContext.Countries
                 .Where(c => code.ToLower().Equals(c.Code!.ToLower()))
                 .FirstOrDefault();
        }
    }
}
