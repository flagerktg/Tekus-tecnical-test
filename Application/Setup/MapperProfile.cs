using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Setup
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            #region Services

            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<ServiceDto, ServiceListResultDto>();

            #endregion

            #region Providers

            CreateMap<Provider, ProviderDto>()
                .ForMember(dest => dest.ServiceIds,
                opt => opt.MapFrom(src => src.Services!.Select(s => s.Id))); 
            CreateMap<ProviderDto, Provider>();
            CreateMap<ProviderDto, ProviderListResultDto>();

            #endregion
        }

    }
}
