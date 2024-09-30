using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICountryService
    {
        Task<ICollection<CountryListResultDto>> GetCountriesFromExternalServiceAsync();
        Country GetCountryByCode(string code);
    }
}
