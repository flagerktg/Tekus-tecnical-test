using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TekusApi.Models;

namespace TekusApi.Controllers
{
    /// <summary>
    /// Controller for managing Provider.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private IMapper Mapper { get; }
        private IProviderService ProviderService { get; }

        /// <summary>Constructor</summary>
        public ProvidersController(
            IMapper mapper,
            IProviderService providerService
        )
        {
            Mapper = mapper;
            ProviderService = providerService;
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
        public void Delete(long id) =>
            ProviderService.Delete(id);


    }
}
