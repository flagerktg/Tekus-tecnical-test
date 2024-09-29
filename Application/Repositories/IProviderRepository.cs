using Application.DTOs;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        /// <summary>
        /// Retrieves a list of providers based on the specified filters.
        /// </summary>
        /// <param name="request">The filters to apply when retrieving providers.</param>
        /// <returns>A collection of providers matching the specified filters.</returns>
        ListResultCollectionDto<ProviderDto> List(ProviderListRequestDto request);

        /// <summary>
        /// Retrieves a summary of providers grouped by country.
        /// </summary>
        /// <returns>A dictionary where the key is the country name, and the value is the count of providers in that country.</returns>
        Dictionary<string, int> GetProvidersByCountry();
    }
}
