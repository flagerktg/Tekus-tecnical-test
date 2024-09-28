namespace Application.DTOs
{
    public class ProviderListRequestDto : ListRequestBaseDto
    {
        public string? Query { get; set; }
        public long? Country { get; set; }
    }
}
