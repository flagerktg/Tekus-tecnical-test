namespace TekusApi.Models
{
    /// <summary>
    /// Model representing a Provider with additional properties
    /// from the creation model.
    /// </summary>
    public class Provider : CreateProvider
    {
        /// <summary>
        /// Unique identifier of the Provider.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
