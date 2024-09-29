namespace TekusApi.Models
{
    /// <summary>
    /// Represents a collection of results with a total count.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public class ListResultCollection<T>
    {
        /// <summary>
        /// Total count of items in the collection.
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Items in the collection.
        /// </summary>
        public IEnumerable<T>? Items { get; set; }
    }
}
