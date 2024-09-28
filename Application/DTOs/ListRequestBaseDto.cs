namespace Application.DTOs
{
    public class ListRequestBaseDto
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public bool? OrderAsc { get; set; }
        public string? OrderBy { get; set; }
    }
}
