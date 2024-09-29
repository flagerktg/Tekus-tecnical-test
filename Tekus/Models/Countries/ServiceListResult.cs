namespace TekusApi.Models
{
    /// <summary>
    /// Represents the result of a Country listing.
    /// </summary>
    public class CountryListResult
    {
        /// <summary>
        /// The unique identifier for the Country.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// the name of the Country.
        /// </summary>
        public string? Name { get; set; }
    }
}
