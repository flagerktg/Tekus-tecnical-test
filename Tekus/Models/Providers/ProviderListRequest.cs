namespace TekusApi.Models
{
    /// <summary>
    /// Represents a request for a list of Provider with optional query filtering.
    /// </summary>
    public class ProviderListRequest : ListRequestBase
    {
        /// <summary>
        /// The query string used to filter Provider.
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        /// The CountryId used to filter Provider.
        /// </summary>
        public long? CountryId { get; set; }
    }
}
