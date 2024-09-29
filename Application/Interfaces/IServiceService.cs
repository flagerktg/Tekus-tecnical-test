using Application.DTOs;

namespace Application.Interfaces
{
    public interface IServiceService : ICrudService<ServiceDto>
    {
        /// <summary>
        /// Lists Service Models
        /// </summary>
        /// <param name="request">Filters to be applied</param>
        /// <returns>Service matching filters specified</returns>
        ListResultCollectionDto<ServiceListResultDto> List(ServiceListRequestDto request);

        /// <summary>
        /// Assign Countries to Service
        /// </summary>
        /// <returns>Service matching filters specified</returns>
        void AssignCountries(long serviceId, List<(string Code, string Name)> countries);

        /// <param name="serviceId">The ID of the service  to unassign the service from.</param>
        /// <param name="countryCode">The country code of the service to unassign.</param>
        /// <exception cref="Exception">Thrown when the country code does not exist.</exception>
        void UnassignCountry(long serviceId, string countryCode);

        /// <summary>
        /// Get Assigned Countries from service
        /// </summary>
        /// <param name="serviceId">Filters to be applied</param>
        /// <returns>Service matching filters specified</returns>
        IEnumerable<CountryDto> GetAssignedCountries(long serviceId);
    }
}
