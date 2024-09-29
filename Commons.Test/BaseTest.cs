using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Infrastructure.Repositories;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TekusApi.Controllers;
using System.Net.Http;

namespace Tekus.Commons.Test
{
    public class BaseTest : WebApplicationFactory<Program>
    {
        protected IServiceProvider ServiceProvider = null!;
        protected IConfiguration? Configuration { get; set; }
        protected IMapper? Mapper { get; set; }

        public BaseTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection()
                .AddSingleton(Configuration!)
                .AddSingleton(
                    new MapperConfiguration(
                        mc =>
                        {
                            mc.AddProfiles(new List<Profile> {
                            new TekusApi.Setup.MapperProfile(),
                            new Application.Setup.MapperProfile()
                        });
                        }).CreateMapper())
                .AddDbContext<SqlServerDbContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddTransient(typeof(Lazy<>));
            services.AddSingleton(typeof(ILogger<>), typeof(FakeLogger<>));

            // Repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            // Services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IServiceService, ServiceService>();

            // Registro de HttpClient para el servicio externo de países.
            services.AddHttpClient<ICountryService, CountryService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ExternalServices:CountryApiUrl"]);
            });

            // Controllers
            services.AddScoped<ServicesController>();
            services.AddScoped<CountriesController>();
            services.AddScoped<ProvidersController>();

            ServiceProvider = services.BuildServiceProvider();
            Mapper = ServiceProvider?.GetRequiredService<IMapper>();
        }
    }
}
