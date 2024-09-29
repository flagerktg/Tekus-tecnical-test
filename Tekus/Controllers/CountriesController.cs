using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TekusApi.Models;

namespace TekusApi.Controllers
{
    /// <summary>
    /// Controller for managing Country.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private IMapper Mapper { get; }
        private ICountryService CountryService { get; }

        /// <summary>Constructor</summary>
        public CountriesController(
            IMapper mapper,
            ICountryService countryService
        )
        {
            Mapper = mapper;
            CountryService = countryService;
        }

        /// <summary>
        /// List Countries from external service.
        /// </summary>
        /// <returns>List of countries retrieved from external service.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await CountryService.GetCountriesFromExternalServiceAsync();
            if (countries == null || !countries.Any())
            {
                return NotFound("No countries found.");
            }
            return Ok(countries);
        }
    }
}
