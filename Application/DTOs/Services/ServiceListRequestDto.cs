namespace Application.DTOs
{
    public class ServiceListRequestDto : ListRequestBaseDto
    {
        public string? Query { get; set; }
        public long? CountryId { get; set; }
    }
}
