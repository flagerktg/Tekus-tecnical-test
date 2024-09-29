using Application.DTOs;
using AutoMapper;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class ProviderService : CrudService<ProviderDto, Provider, IProviderRepository>, IProviderService
    {
        public ProviderService(
            IMapper mapper,
            IProviderRepository repository
        ) : base(mapper, repository)
        {
        }
        public ListResultCollectionDto<ProviderListResultDto> List(ProviderListRequestDto request) =>
            Mapper.Map<ListResultCollectionDto<ProviderListResultDto>>(
                Repository.List(request)
            );
    }
}
