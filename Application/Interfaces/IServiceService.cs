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
    }
}
