using Bogus;
using TekusApi.Models;
using Xunit;

namespace TekusApi.Test.Mocks
{
    public static class ServiceMock
    {
        /// <summary>
        /// Crea un objeto 'CreateService' mockeado usando la biblioteca 'Bogus'.
        /// </summary>
        /// <param name="providerId">ID del proveedor asociado.</param>
        /// <param name="priceByHour">Precio por hora del servicio.</param>
        /// <returns>Un objeto 'CreateService' mockeado.</returns>
        public static CreateService Create(long providerId, decimal priceByHour) =>
            new Faker<CreateService>()
                .RuleFor(c => c.Name, f => f.Company.CompanyName())
                .RuleFor(c => c.ProviderId, providerId)
                .RuleFor(c => c.PriceByHour, priceByHour);

        /// <summary>
        /// Compara dos instancias de 'CreateService' para asegurar que sean iguales.
        /// </summary>
        /// <param name="expected">El objeto esperado.</param>
        /// <param name="actual">El objeto real.</param>
        public static void Equal(CreateService expected, CreateService actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.ProviderId, actual.ProviderId);
            Assert.Equal(expected.PriceByHour, actual.PriceByHour);
        }
    }
}
