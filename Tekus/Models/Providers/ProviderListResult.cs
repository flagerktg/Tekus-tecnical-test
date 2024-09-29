namespace TekusApi.Models
{
    /// <summary>
    /// Represents the result of a Provider listing.
    /// </summary>
    public class ProviderListResult
    {
        /// <summary>
        /// The unique identifier for the Provider.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The NIT of the Provider.
        /// </summary>
        public string? Nit { get; set; }

        /// <summary>
        /// The name of the Provider.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The email address of the Provider.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// services Ids provided by the Provider.
        /// </summary>
        public IEnumerable<long>? ServiceIds { get; set; }
    }
}
