using Application.DTOs;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        /// <summary>
        /// Lists Providers
        /// </summary>
        /// <param name="request">Filters to be applied</param>
        /// <returns>Providers matching filters specified</returns>
        ListResultCollectionDto<ProviderDto> List(ProviderListRequestDto request);

    }
}
