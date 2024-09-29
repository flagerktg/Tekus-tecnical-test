using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProviderService : ICrudService<ProviderDto>
    {
        /// <summary>
        /// Lists Providers
        /// </summary>
        /// <param name="request">Filters to be applied</param>
        /// <returns>Providers matching filters specified</returns>
        ListResultCollectionDto<ProviderListResultDto> List(ProviderListRequestDto request);

        /// <summary>
        /// Summary Result
        /// </summary>
        /// <returns>Providers And Services Summary</returns>
        SummaryResultDto GetProvidersAndServicesSummary();
    }
}
