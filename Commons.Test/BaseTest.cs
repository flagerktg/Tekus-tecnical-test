using AutoMapper;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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
                            new Api.Setup.MapperProfile(),
                            new Application.Setup.MapperProfile()
                        });
                        }).CreateMapper())
                .AddDbContext<SqlServerDbContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("Connection")));


            services.AddTransient(typeof(Lazy<>));
            services.AddSingleton(typeof(ILogger<>), typeof(FakeLogger<>));

            // Repositories
            //services.AddScoped<IAlertConfigurationRepository, AlertConfigurationRepository>();
            //services.AddScoped<ICenterRepository, CenterRepository>();


            // Services
            //services.AddScoped<IAlertConfigurationService, AlertConfigurationService>();
            //services.AddScoped<ICenterService, CenterService>();


            // Controllers
            //services.AddScoped<AuthController>();
            //services.AddScoped<CentersController>();
    

            ServiceProvider = services.BuildServiceProvider();
            Mapper = ServiceProvider?.GetRequiredService<IMapper>();

        }
    }
}
