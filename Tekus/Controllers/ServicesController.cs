using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekusApi.Models;

namespace TekusApi.Controllers
{
    /// <summary>
    /// Controller for managing Service.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private IMapper Mapper { get; }
        private IServiceService ServiceService { get; }

        /// <summary>Constructor</summary>
        public ServicesController(
            IMapper mapper,
            IServiceService serviceService
        )
        {
            Mapper = mapper;
            ServiceService = serviceService;
        }

        /// <summary>
        /// List services based on a set of filters
        /// </summary>
        /// <param name="model">Filters to be applied</param>
        /// <returns>Services matching filters provided</returns>
        [HttpGet]
        public ListResultCollection<ServiceListResult> List([FromQuery] ServiceListRequest model) =>
            Mapper.Map<ListResultCollection<ServiceListResult>>(
                ServiceService.List(
                    Mapper.Map<ServiceListRequestDto>(
                        model
                    )
                )
            );

        /// <summary>
        /// Creates a new Service
        /// </summary>
        /// <param name="model">Service information</param>
        /// <returns>Id of Service</returns>
        [HttpPost]
        public long Create(CreateService model) =>
            ServiceService.Create(
                Mapper.Map<ServiceDto>(
                    model
                )
            );

        /// <summary>
        /// Gets a Service by its Id
        /// </summary>
        /// <param name="id">Id of the Service</param>
        /// <returns>The requested Service</returns>
        [HttpGet("{id}")]
        public Service Read(long id) =>
            Mapper.Map<Service>(
                ServiceService.Read(id)
            );

        /// <summary>
        /// Updates an existing Service
        /// </summary>
        /// <param name="id">Service id</param>
        /// <param name="model">Updated Service information</param>
        /// <returns>OK if entity updated</returns>
        [HttpPut]
        public void Update(long id, CreateService model) =>
            ServiceService.Update(
                Mapper.Map(
                    model,
                    new ServiceDto
                    {
                        Id = id
                    }
                )
            );

        /// <summary>
        /// Deletes a Service by its Id
        /// </summary>
        /// <param name="id">Id of the Service to delete</param>
        /// <returns>OK if entity removed</returns>
        [HttpDelete("{id}")]
        public void Delete(long id) =>
            ServiceService.Delete(id);

        /// <summary>
        /// Assigns countries to a service
        /// </summary>
        /// <param name="serviceId">Id of the Service</param>
        /// <param name="countryCodes">List of country codes to assign</param>
        [HttpPost("{serviceId}/countries")]
        public void AssignCountries(long serviceId, List<(string Code, string Name)> countries) =>
            ServiceService.AssignCountries(serviceId, countries);

        /// <summary>
        /// Get the list of countries assigned to a service
        /// </summary>
        /// <param name="serviceId">Id of the Service</param>
        /// <returns>List of country codes</returns>
        [HttpGet("{serviceId}/countries")]
        public IEnumerable<CountryDto> GetCountries(long serviceId) =>
            ServiceService.GetAssignedCountries(serviceId);
    }
}
