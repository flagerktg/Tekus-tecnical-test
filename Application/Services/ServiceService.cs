using Application.DTOs;
using AutoMapper;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class ServiceService : CrudService<ServiceDto, Service, IServiceRepository>, IServiceService
    {
        public ServiceService(
            IMapper mapper,
            IServiceRepository repository
        ) : base(mapper, repository)
        {
        }
        public ListResultCollectionDto<ServiceListResultDto> List(ServiceListRequestDto request) =>
            Mapper.Map<ListResultCollectionDto<ServiceListResultDto>>(
                Repository.List(request)
            );
    }
}
