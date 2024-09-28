using AutoMapper;
using Deal2.Api.Controllers;
using Deal2.Application.Interfaces;
using Deal2.Application.Repositories;
using Deal2.Application.Services;
using Deal2.Domain.Entities;
using Deal2.Domain.Exceptions;
using Deal2.Infrastructure.Mongo;
using Deal2.Infrastructure.Repositories;
using Deal2.Infrastructure.SQLServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Deal2.Commons.Test
{
    public class BaseTest : WebApplicationFactory<Program>
    {
        private readonly string ENVIRONMENT = "test";
        private readonly string APP_CONFIGURATION = "Endpoint=https://deal2appconfiguration.azconfig.io;Id=Frfj;Secret=CFmYJxiMBrJ1R5ktQADBGMJax5XnYiT1opz0DxzVr5Nj16TAe1WRJQQJ99AFACi5YpzqagX5AAACAZACb9X2";
        private const string MONGO_CONNECTION_STRING = "ConnectionStrings:MongoConnectionString";
        private const string MONGO_DBNAME = "ConnectionStrings:MongoDBName";
        private const string MONGO_SERVER = "ConnectionStrings:MongoServer";
        private const string MONGO_USER = "ConnectionStrings:MongoUserName";
        private const string MONGO_PASSWORD = "ConnectionStrings:MongoPassword";

        protected IServiceProvider ServiceProvider = null!;
        protected IConfiguration? Configuration { get; set; }
        protected IMapper? Mapper { get; set; }

        public BaseTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddAzureAppConfiguration(options => options.Connect(APP_CONFIGURATION).Select(KeyFilter.Any, ENVIRONMENT))
                .AddUserSecrets<BaseTest>()
                .Build();

            var services = new ServiceCollection()
                .AddSingleton(Configuration!)
                .AddSingleton(
                    new MapperConfiguration(
                        mc =>
                        {
                            mc.AddProfiles(new List<Profile> {
                            new Infrastructure.Setup.MapperProfile(),
                            new Api.Setup.MapperProfile(),
                            new Application.Setup.MapperProfile()
                        });
                        }).CreateMapper())
                .AddDbContext<SqlServerDbContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("SQLServerDB")));


            // Mongo database
            var mongoConnectionString = Configuration.GetValue<string>(MONGO_CONNECTION_STRING);
            var mongoDatabase = Configuration.GetValue<string>(MONGO_DBNAME) ?? throw new Deal2Exception("Mongo database is not configured correctly");

            if (string.IsNullOrEmpty(mongoConnectionString))
            {
                var server = Configuration.GetValue<string>(MONGO_SERVER);
                var user = Configuration.GetValue<string>(MONGO_USER);
                var password = Configuration.GetValue<string>(MONGO_PASSWORD);

                if (!string.IsNullOrEmpty(server))
                {
                    services.AddScoped(c => new MongoDbContext(ENVIRONMENT, null, mongoDatabase, server, user, password));
                }
                else throw new Exception("MongoDB is not configured correctly");
            }
            else
            {
                services.AddScoped(c => new MongoDbContext(ENVIRONMENT, mongoConnectionString, mongoDatabase, null, null, null));
            }

            services.AddTransient(typeof(Lazy<>));
            services.AddSingleton(typeof(ILogger<>), typeof(FakeLogger<>));

            // Repositories
            services.AddScoped<IAlertConfigurationRepository, AlertConfigurationRepository>();
            services.AddScoped<ICenterRepository, CenterRepository>();
            services.AddScoped<IConsumableRepository, ConsumableRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICycleTypeRepository, CycleTypeRepository>();
            services.AddScoped<IEnvironmentRepository, EnvironmentRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IInstalledMachineImageRepository, InstalledMachineImageRepository>();
            services.AddScoped<IInstalledMachineRepository, InstalledMachineRepository>();
            services.AddScoped<IInstalledPlcRepository, InstalledPlcRepository>();
            services.AddScoped<IMachineDocRepository, MachineDocRepository>();
            services.AddScoped<IMachineImageRepository, MachineImageRepository>();
            services.AddScoped<IMachineModelRepository, MachineModelRepository>();
            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IPlcModelRepository, PlcModelRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IAlertConfigurationService, AlertConfigurationService>();
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<IConsumableService, ConsumableService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICycleTypeService, CycleTypeService>();
            services.AddScoped<IEnvironmentService, EnvironmentService>();
            services.AddScoped<IFileService, AzureBlobStorageFileService>();
            services.AddScoped<IInstalledMachineService, InstalledMachineService>();
            services.AddScoped<IInstalledPlcService, InstalledPlcService>();
            services.AddScoped<IMachineModelService, MachineModelService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IPlantService, PlantService>();
            services.AddScoped<IPlcModelService, PlcModelService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Controllers
            services.AddScoped<AuthController>();
            services.AddScoped<CentersController>();
            services.AddScoped<ClientsController>();
            services.AddScoped<ConsumablesController>();
            services.AddScoped<ContactsController>();
            services.AddScoped<CycleTypesController>();
            services.AddScoped<ClientsController>();
            services.AddScoped<EnvironmentsController>();
            services.AddScoped<FilesController>();
            services.AddScoped<InstalledMachinesController>();
            services.AddScoped<InstalledPlcsController>();
            services.AddScoped<MachineModelsController>();
            services.AddScoped<PlacesController>();
            services.AddScoped<PlantsController>();
            services.AddScoped<PlcModelsController>();
            services.AddScoped<RolesController>();
            services.AddScoped<UsersController>();
            services.AddScoped<AlertConfigurationsController>();

            ServiceProvider = services.BuildServiceProvider();
            Mapper = ServiceProvider?.GetRequiredService<IMapper>();

        }
    }
}
