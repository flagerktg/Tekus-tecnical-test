using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekusApi.Models;

namespace TekusApi.Controllers
{
    /// <summary>
    /// Controller for managing Country.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private ICountryService CountryService { get; }

        /// <summary>Constructor</summary>
        public CountriesController(
            ICountryService countryService
        )
        {
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
            if (countries == null || countries.Count == 0)
            {
                return NotFound("No countries found.");
            }
            return Ok(countries);
        }
    }
}
