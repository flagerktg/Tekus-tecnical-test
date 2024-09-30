using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekusApi.Models;

namespace TekusApi.Controllers
{
    /// <summary>
    /// Controller for managing Provider.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private IMapper Mapper { get; }
        private IProviderService ProviderService { get; }
        private IServiceService ServiceService { get; }

        /// <summary>Constructor</summary>
        public ProvidersController(
            IMapper mapper,
            IProviderService providerService,
            IServiceService serviceService
        )
        {
            Mapper = mapper;
            ProviderService = providerService;
            ServiceService = serviceService;
        }

        /// <summary>
        /// List providers based on a set of filters
        /// </summary>
        /// <param name="model">Filters to be applied</param>
        /// <returns>Providers matching filters provided</returns>
        [HttpGet]
        public ListResultCollection<ProviderListResult> List([FromQuery] ProviderListRequest model) =>
            Mapper.Map<ListResultCollection<ProviderListResult>>(
                ProviderService.List(
                    Mapper.Map<ProviderListRequestDto>(
                        model
                    )
                )
            );

        /// <summary>
        /// Creates a new Provider
        /// </summary>
        /// <param name="model">Provider information</param>
        /// <returns>Id of Provider</returns>
        [HttpPost]
        public long Create(CreateProvider model) =>
            ProviderService.Create(
                Mapper.Map<ProviderDto>(
                    model
                )
            );

        /// <summary>
        /// Gets a Provider by its Id
        /// </summary>
        /// <param name="id">Id of the Provider</param>
        /// <returns>The requested Provider</returns>
        [HttpGet("{id}")]
        public Provider Read(long id) =>
            Mapper.Map<Provider>(
                ProviderService.Read(id)
            );

        /// <summary>
        /// Updates an existing Provider
        /// </summary>
        /// <param name="id">Provider id</param>
        /// <param name="model">Updated Provider information</param>
        /// <returns>OK if entity updated</returns>
        [HttpPut]
        public void Update(long id, CreateProvider model) =>
            ProviderService.Update(
                Mapper.Map(
                    model,
                    new ProviderDto
                    {
                        Id = id
                    }
                )
            );

        /// <summary>
        /// Deletes a Provider by its Id
        /// </summary>
        /// <param name="id">Id of the Provider to delete</param>
        /// <returns>OK if entity removed</returns>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var services = ServiceService.List(new ServiceListRequestDto { ProviderId = id });

            if (services.TotalCount > 0)
                throw new TekusException("this provider is already used in one or more services");

            ProviderService.Delete(id);
        }

        /// <summary>
        /// Get summary of providers and services by country
        /// </summary>
        /// <returns>Summary result of providers and services by country</returns>
        [HttpGet("summary")]
        public SummaryResultDto GetSummary() =>
            ProviderService.GetProvidersAndServicesSummary();

    }
}
