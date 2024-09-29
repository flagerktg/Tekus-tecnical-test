using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICountryService
    {
        Task<ICollection<CountryListResultDto>> GetCountriesFromExternalServiceAsync();
    }
}
