using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Tekus.Commons.Test;
using TekusApi.Controllers;
using TekusApi.Models;
using TekusApi.Test.Mocks;
using Xunit;
using Moq;
using Application.Interfaces;
using Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using TekusApi.Models.Countries;

namespace Tekus.Api.Test
{
    public class ProviderServiceTest : BaseTest
    {
        // Simular la obtención de los servicios necesarios
        public ServicesController ServicesController { get => ServiceProvider.GetRequiredService<ServicesController>(); }
        public ProvidersController ProvidersController { get => ServiceProvider.GetRequiredService<ProvidersController>(); }

        // Inyectar el mock de ICountryService para simular la API externa de países
        private Mock<ICountryService> _mockCountryService;

        public ProviderServiceTest()
        {
            _mockCountryService = new Mock<ICountryService>();

            // Simular la respuesta del servicio de países
            _mockCountryService.Setup(service => service.GetCountriesFromExternalServiceAsync())
                .ReturnsAsync(new List<CountryListResultDto>
                {
                    new CountryListResultDto { Code = "GS", Name = "South Georgia" },
                    new CountryListResultDto { Code = "AR", Name = "Argentina" }
                });

            // Reemplazar el servicio original con el mock
            ServiceProvider.GetRequiredService<ICountryService>();
        }

        [Fact]
        public async Task CRUD_Provider_With_Services_And_Countries()
        {
            // Obtener los países desde el servicio simulado
            var countries = (await _mockCountryService.Object.GetCountriesFromExternalServiceAsync()).ToList();
            CountryListResultDto country = countries[0];

            // Crear un proveedor
            CreateProvider providerModel = ProviderMock.Create();
            long providerId = ProvidersController.Create(providerModel);
            Assert.True(providerId > 0);

            // Crear un servicio y asignarlo a un proveedor
            CreateService serviceModel = ServiceMock.Create(providerId, 100);
            long serviceId = ServicesController.Create(serviceModel);
            Assert.True(serviceId > 0);

            // Asociar el servicio con países simulados (usando código y nombre)
            ServicesController.AssignCountries(serviceId, new List<(string Code, string Name)>
                {
                    (country.Code!, country.Name!)
                });

            // Validar que el país fue asignado correctamente
            var serviceCountries = ServicesController.GetCountries(serviceId);
            Assert.Contains(serviceCountries, sc => sc.Code == country.Code);

            //Leer proveedor para validar que tiene el servicio asociado
            Provider providerRead = ProvidersController.Read(providerId);
            Assert.Contains(serviceId, providerRead.ServiceIds!);

            //// Actualizar el servicio
            CreateService serviceUpdateModel = ServiceMock.Create(providerId, 120);
            ServicesController.Update(serviceId, serviceUpdateModel);

            //// Verificar la actualización
            Service serviceUpdated = ServicesController.Read(serviceId);
            Assert.Equal(serviceUpdateModel.PriceByHour, serviceUpdated.PriceByHour);

            //// Eliminar el servicio
            ServicesController.Delete(serviceId);
            Assert.Throws<TekusException>(() => ServicesController.Read(serviceId));

            //// Eliminar el proveedor
            ProvidersController.Delete(providerId);
            Assert.Throws<TekusException>(() => ProvidersController.Read(providerId));
        }

    }
}
