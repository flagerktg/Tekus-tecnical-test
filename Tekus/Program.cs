using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Application.Interfaces;
using Application.Services;
using Application.Repositories;
using Infrastructure.Repositories;
using TekusApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Registro de HttpClient para el servicio externo de pa�ses.
builder.Services.AddHttpClient<ICountryService, CountryService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:CountryApiUrl"]);
});

// Configurar la cadena de conexi�n desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("Connection");

// Configura el DbContext para usar SQL Server
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar autenticaci�n JWT
// Registrar JwtService como un servicio singleton
builder.Services.AddSingleton<JwtService>();

// Configurar autenticaci�n JWT
var jwtConfig = builder.Configuration.GetSection("JwtConfig");
var secretKey = jwtConfig["Secret"];
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; 
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, 
        IssuerSigningKey = new SymmetricSecurityKey(key), 
        ValidateIssuer = false, 
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero 
    };
});

// A�adir autorizaci�n
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(options =>
{
    // Definir el esquema de seguridad para JWT
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingresa tu token JWT en este formato: Bearer {tu_token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    // A�adir la definici�n de seguridad a Swagger
    options.AddSecurityDefinition("Bearer", securityScheme);
    // Definir los requisitos de seguridad para Swagger
    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };
    options.AddSecurityRequirement(securityRequirement);
});

// Registrar AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registrar Lazy inyecci�n de dependencias
builder.Services.AddTransient(typeof(Lazy<>));

// Registrar servicios y repositorios
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ICountryService, CountryService>();

// Registrar configuraci�n de AutoMapper con perfiles adicionales
builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfiles(new Profile[]
    {
        new TekusApi.Setup.MapperProfile(),
        new Application.Setup.MapperProfile(),
    });
}).CreateMapper());

builder.Services.AddControllers();

// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de la aplicaci�n
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
