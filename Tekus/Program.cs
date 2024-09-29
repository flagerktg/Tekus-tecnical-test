using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Application.Interfaces;
using Application.Services;
using Application.Repositories;
using Infrastructure.Repositories;  // Asegúrate de que el espacio de nombres de tu servicio esté correcto

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Connection");

// Configura el DbContext para usar SQL Server
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configura la autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Auth:Jwt:Keys"]?.Split(",")[0]; 
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT Key is not configured properly.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Auth:Jwt:Issuer"],
            ValidAudience = builder.Configuration["Auth:Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)) 
        };
    });

// Registrar AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient(typeof(Lazy<>));

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();  

builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfiles(new Profile[]
    {
                    new TekusApi.Setup.MapperProfile(),
                    new Application.Setup.MapperProfile(),
    });
}).CreateMapper());

// Agrega controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar autenticación y autorización
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
