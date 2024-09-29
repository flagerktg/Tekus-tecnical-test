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
        }

    }
}
