namespace Api.Models
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
        /// Price By hour of the service.
        /// </summary>
        public decimal? PriceByhour { get; set; }
    }
}
