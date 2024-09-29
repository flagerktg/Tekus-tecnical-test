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

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
