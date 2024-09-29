namespace TekusApi.Models
{
    /// <summary>
    /// Represents the result of a Service listing.
    /// </summary>
    public class ServiceListResult
    {
        /// <summary>
        /// The unique identifier for the Service.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// the name of the Service.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The Price By Hour of the Service.
        /// </summary>
        public decimal? PriceByHour { get; set; }
    }
}
