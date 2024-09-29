using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _externalServiceUrl;

        public CountryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new TekusException("HttpClient instance is null");
            _externalServiceUrl = configuration["ExternalServices:CountryApiUrl"]
                                  ?? throw new TekusException("ExternalServices:CountryApiUrl configuration is missing");
        }

        public async Task<ICollection<CountryListResultDto>> GetCountriesFromExternalServiceAsync()
        {
            var response = await _httpClient.GetAsync(_externalServiceUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error retrieving countries from external service: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar la respuesta JSON a una lista de objetos temporales
            var countries = JsonSerializer.Deserialize<IEnumerable<CountryApiResponse>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (countries == null)
            {
                throw new TekusException("Error deserializing the countries from the external service.");
            }

            // Mapear los países a CountryListResultDto y devolver la lista
            return countries.Select(c => new CountryListResultDto
            {
                Code = c.Cca2 ?? string.Empty,  // El código del país (ej: "US", "AR", etc.)
                Name = c.Name?.Common ?? string.Empty  // El nombre común del país
            }).ToList();
        }


        public class CountryApiResponse
        {
            public NameInfo? Name { get; set; }
            public string? Cca2 { get; set; }  
        }

        public class NameInfo
        {
            public string? Common { get; set; }  
        }

    }
}
