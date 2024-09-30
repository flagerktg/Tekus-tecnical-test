using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Diagnostics.Metrics;
using Tekus.Commons.Test;
using TekusApi.Controllers;
using TekusApi.Models;
using TekusApi.Test.Mocks;
using Xunit;

namespace Tekus.Api.Test
{
    public class ProviderServiceTest : BaseTest
    {
        public ServicesController ServicesController { get => ServiceProvider.GetRequiredService<ServicesController>(); }
        public ProvidersController ProvidersController { get => ServiceProvider.GetRequiredService<ProvidersController>(); }

        // Inyectar el mock de ICountryService para simular la API externa de países
        private readonly Mock<ICountryService> _mockCountryService;

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
            ServicesController.AssignCountries(serviceId, [country.Code!]);

            // Validar que el país fue asignado correctamente
            var serviceCountries = ServicesController.GetCountries(serviceId);
            Assert.Contains(serviceCountries, sc => sc.Code == country.Code);

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

        [Fact]
        public async Task Summary_Indicators_Providers_And_Services_By_Country()
        {
            // Simular la obtención de países desde el servicio
            var countries = (await _mockCountryService.Object.GetCountriesFromExternalServiceAsync()).ToList();
            var country1 = countries[0];
            var country2 = countries[1];

            // Crear dos proveedores
            long providerId1 = ProvidersController.Create(ProviderMock.Create());
            long providerId2 = ProvidersController.Create(ProviderMock.Create());
            Assert.True(providerId1 > 0);
            Assert.True(providerId2 > 0);

            // Crear servicios para los proveedores
            long serviceId1 = ServicesController.Create(ServiceMock.Create(providerId1, 150));
            long serviceId2 = ServicesController.Create(ServiceMock.Create(providerId2, 200));
            Assert.True(serviceId1 > 0);
            Assert.True(serviceId2 > 0);

            // Asociar los servicios con los países simulados
            ServicesController.AssignCountries(serviceId1, [country1.Code!]);
            ServicesController.AssignCountries(serviceId2, [country2.Code!]);

            // Obtener el resumen de indicadores
            var summary = ProvidersController.GetSummary();

            // Validar que los proveedores creados están en el resumen
            Assert.True(summary.ProvidersByCountry.ContainsKey(country1.Name!) && summary.ProvidersByCountry[country1.Name!] > 0);
            Assert.True(summary.ProvidersByCountry.ContainsKey(country2.Name!) && summary.ProvidersByCountry[country2.Name!] > 0);

            // Validar que los servicios creados están en el resumen
            Assert.True(summary.ServicesByCountry.ContainsKey(country1.Name!) && summary.ServicesByCountry[country1.Name!] > 0);
            Assert.True(summary.ServicesByCountry.ContainsKey(country2.Name!) && summary.ServicesByCountry[country2.Name!] > 0);
        }


    }
}
