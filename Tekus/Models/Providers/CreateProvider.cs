namespace TekusApi.Models
{
    /// <summary>
    /// Model used for creating a new Provider.
    /// </summary>
    public class CreateProvider
    {
        /// <summary>
        /// NIT of the Provider.
        /// </summary>
        public string? Nit { get; set; }

        /// <summary>
        /// Name of the Provider.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Email address of the Provider.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// List of Service IDs associated with the Provider.
        /// </summary>
        public IEnumerable<long>? ServiceIds { get; set; }

        /// <summary>
        /// Optional personalized fields for the Provider.
        /// </summary>
        public Dictionary<string, string>? PersonalizedFields { get; set; }
    }
}
