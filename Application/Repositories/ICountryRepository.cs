using Domain.Entities;

namespace Application.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IEnumerable<Country> GetCountriesByCodes(IEnumerable<string> countryCodes);
        Country? GetCountryByCode(string code);

    }
}
