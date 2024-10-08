﻿namespace TekusApi.Models
{
    /// <summary>
    /// Represents a request for a list of Services with optional query filtering.
    /// </summary>
    public class ServiceListRequest : ListRequestBase
    {
        /// <summary>
        /// The query string used to filter Services.
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        /// The CountryId used to filter Service.
        /// </summary>
        public long? CountryId { get; set; }
    }
}
