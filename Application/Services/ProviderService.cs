using Application.DTOs;
using AutoMapper;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class ProviderService : CrudService<ProviderDto, Provider, IProviderRepository>, IProviderService
    {
        private readonly IServiceRepository _serviceRepository;

        public ProviderService(
            IMapper mapper,
            IProviderRepository repository,
            IServiceRepository serviceRepository 
        ) : base(mapper, repository)
        {
            _serviceRepository = serviceRepository; 
        }

        public ListResultCollectionDto<ProviderListResultDto> List(ProviderListRequestDto request) =>
            Mapper.Map<ListResultCollectionDto<ProviderListResultDto>>(
                Repository.List(request)
            );

        public SummaryResultDto GetProvidersAndServicesSummary()
        {
            var providersByCountry = Repository.GetProvidersByCountry();

            var servicesByCountry = _serviceRepository.GetServicesByCountry();

            return new SummaryResultDto
            {
                ProvidersByCountry = providersByCountry,
                ServicesByCountry = servicesByCountry
            };
        }
    }
}
