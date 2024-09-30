namespace TekusApi.Models
{
    /// <summary>
    /// Model used for creating a new Service item.
    /// </summary>
    public class CreateService
    {
        /// <summary>
        /// Name of the Service.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Price by hour of the Service.
        /// </summary>
        public decimal? PriceByHour { get; set; }

        /// <summary>
        /// Provider Id of the Service.
        /// </summary>
        public long ProviderId { get; set; }
    }
}
