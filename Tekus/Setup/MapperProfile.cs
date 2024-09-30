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

            #region Countries

            CreateMap<Application.DTOs.CountryDto, TekusApi.Models.Country>().ReverseMap();

            #endregion

            #region Services

            CreateMap<TekusApi.Models.CreateService, Application.DTOs.ServiceDto>();
            CreateMap<Application.DTOs.ServiceDto, TekusApi.Models.Service>()
                .ReverseMap();
            CreateMap<TekusApi.Models.ServiceListRequest, Application.DTOs.ServiceListRequestDto>();
            CreateMap<Application.DTOs.ServiceListResultDto, TekusApi.Models.ServiceListResult>();

            #endregion

            #region Providers

            CreateMap<TekusApi.Models.CreateProvider, Application.DTOs.ProviderDto>();
            CreateMap<Application.DTOs.ProviderDto, TekusApi.Models.Provider>()
                .ReverseMap();
            CreateMap<TekusApi.Models.ProviderListRequest, Application.DTOs.ProviderListRequestDto>();
            CreateMap<Application.DTOs.ProviderListResultDto, TekusApi.Models.ProviderListResult>();

            #endregion

            #region Common

            CreateMap(typeof(Application.DTOs.ListResultCollectionDto<>), typeof(TekusApi.Models.ListResultCollection<>));

            #endregion
        }
    }
}
