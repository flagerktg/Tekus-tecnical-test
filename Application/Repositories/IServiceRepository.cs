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

    }
}
