namespace TekusApi.Models
{
    /// <summary>
    /// Represents the base class for a list request, including pagination and sorting options.
    /// </summary>
    public class ListRequestBase
    {
        /// <summary>
        /// Maximum number of items to return.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Number of items to skip before starting to return items.
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Value indicating whether the sorting should be in ascending order.
        /// </summary>
        public bool? OrderAsc { get; set; }

        /// <summary>
        /// The property by which the results should be ordered.
        /// </summary>
        public string? OrderBy { get; set; }
    }
}
