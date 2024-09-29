using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Tekus.Commons.Test;
using TekusApi.Controllers;
using TekusApi.Models;
using TekusApi.Test.Mocks;
using Xunit;

namespace Tekus.Api.Test
{
    public class ServiceTest : BaseTest
    {
        public ServicesController Controller { get => ServiceProvider.GetRequiredService<ServicesController>(); }

        [Fact]
        public void CRUD()
        {
            CreateService model = ServiceMock.Create(1, 20);
            long serviceId = Controller.Create(model);
            Assert.True(serviceId > 0);

            Service model2 = Controller.Read(serviceId);

            ServiceMock.Equal(JsonConvert.DeserializeObject<Service>(JsonConvert.SerializeObject(model))!, model2);

            CreateService modelUpdate = ServiceMock.Create(1, 30);
            Controller.Update(serviceId, modelUpdate);

            Service model3 = Controller.Read(serviceId);
            ServiceMock.Equal(JsonConvert.DeserializeObject<Service>(JsonConvert.SerializeObject(modelUpdate))!, model3);

            Controller.Delete(serviceId);
            Assert.Throws<TekusException>(() => Controller.Read(serviceId));

        }
    }
}
