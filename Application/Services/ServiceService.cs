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
        private readonly ICountryRepository _countryRepository;

        public ServiceService(
            IMapper mapper,
            IServiceRepository repository,
            ICountryRepository countryRepository
        ) : base(mapper, repository)
        {
            _countryRepository = countryRepository;
        }
        public ListResultCollectionDto<ServiceListResultDto> List(ServiceListRequestDto request) =>
            Mapper.Map<ListResultCollectionDto<ServiceListResultDto>>(
                Repository.List(request)
            );

        public void AssignCountries(long serviceId, List<(string Code, string Name)> countries)
        {
            // Verificar si el servicio existe
            Service service = CheckEntity(serviceId);

            service.Countries!.Clear();

            foreach (var (code, name) in countries)
            {
                // Verificar si el país ya existe en la base de datos
                var existingCountry = _countryRepository.Get(c => c.Code == code).FirstOrDefault();
                if (existingCountry == null)
                {
                    // Si el país no existe, crearlo
                    var newCountry = new Country
                    {
                        Code = code,
                        Name = name
                    };

                    _countryRepository.Create(newCountry);
                    _countryRepository.Save();

                    existingCountry = newCountry; // Asignar el nuevo país como el existente
                }

                // Asignar el país al servicio
                service.Countries.Add(existingCountry);
            }

            Repository.Save();
        }

        public IEnumerable<CountryDto> GetAssignedCountries(long serviceId)
        {
            var service = CheckEntity(serviceId);

            return service.Countries!.Select(c => new CountryDto
            {
                Code = c.Code,
                Name = c.Name
            });
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
