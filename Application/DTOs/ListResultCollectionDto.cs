namespace Application.DTOs
{
    public class ListResultCollectionDto<T>
    {
        public int? TotalCount { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}
