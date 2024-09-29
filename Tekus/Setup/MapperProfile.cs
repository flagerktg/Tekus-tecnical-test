using AutoMapper;

namespace TekusApi.Setup
{
    /// <summary>
    /// Defines mapping profiles for AutoMapper.
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {

            #region Services

            CreateMap<TekusApi.Models.CreateService, Application.DTOs.ServiceDto>();
            CreateMap<Application.DTOs.ServiceDto, TekusApi.Models.Service>()
                .ReverseMap();
            CreateMap<TekusApi.Models.ServiceListRequest, Application.DTOs.ServiceListRequestDto>();
            CreateMap<Application.DTOs.ServiceListResultDto, TekusApi.Models.ServiceListResult>();

            #endregion
        }
    }
}
