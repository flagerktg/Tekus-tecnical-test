using Application.DTOs;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        /// <summary>
        /// Lists Services
        /// </summary>
        /// <param name="request">Filters to be applied</param>
        /// <returns>Services matching filters specified</returns>
        ListResultCollectionDto<ServiceDto> List(ServiceListRequestDto request);

        /// <summary>
        /// Retrieves a summary of services grouped by country.
        /// </summary>
        /// <returns>A dictionary where the key is the country name, and the value is the count of services in that country.</returns>
        Dictionary<string, int> GetServicesByCountry();

        /// <summary>
        /// Retrieves a list of countries assigned to a specific service, grouped by the service's ID.
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service.</param>
        /// <returns>A collection of countries associated with the specified service.</returns>
        IEnumerable<Country> GetCountriesByServiceId(long serviceId);
    }
}
