namespace TekusApi.Models
{
    /// <summary>
    /// Model representing a Service with additional properties
    /// from the creation model.
    /// </summary>
    public class Service : CreateService
    {
        /// <summary>
        /// Unique identifier of the Service.
        /// </summary>
        public long Id { get; set; }

    }
}
