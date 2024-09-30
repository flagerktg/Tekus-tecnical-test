using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceService : CrudService<ServiceDto, Service, IServiceRepository>, IServiceService
    {
        private readonly ICountryService _countryService;

        public ServiceService(
            IMapper mapper,
            IServiceRepository repository,
            ICountryService countryService
        ) : base(mapper, repository)
        {
            _countryService = countryService;
        }
        public ListResultCollectionDto<ServiceListResultDto> List(ServiceListRequestDto request) =>
            Mapper.Map<ListResultCollectionDto<ServiceListResultDto>>(
                Repository.List(request)
            );

        public void AssignCountries(long serviceId, List<string> countryCodes)
        {
            // Verificar si el servicio existe
            Service service = CheckEntity(serviceId);

            service.Countries!.Clear();

            foreach (var code in countryCodes)
            {
                var existingCountry = _countryService.GetCountryByCode(code); 
                
                service.Countries.Add(existingCountry);
            }

            Repository.Save();
        }

        public IEnumerable<CountryDto> GetAssignedCountries(long serviceId)
        {
            CheckEntity(serviceId);

            return Mapper.Map<IEnumerable<CountryDto>>(Repository.GetCountriesByServiceId(serviceId));
        }

        public void UnassignCountry(long serviceId, string countryCode)
        {
            Service service = CheckEntity(serviceId);

            var countryToUnassign = service.Countries!.FirstOrDefault(c => c.Code == countryCode);

            if (countryToUnassign == null)
            {
                throw new TekusException($"El país con código {countryCode} no está asignado al servicio con id {serviceId}.");
            }

            service.Countries!.Remove(countryToUnassign);

            Repository.Save();
        }
    }
}
