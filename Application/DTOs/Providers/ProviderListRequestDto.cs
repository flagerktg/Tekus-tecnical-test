namespace Application.DTOs
{
    public class ProviderListRequestDto : ListRequestBaseDto
    {
        public string? Query { get; set; }
        public long? CountryId { get; set; }
    }
}
