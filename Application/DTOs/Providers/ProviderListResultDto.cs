namespace Application.DTOs
{
    public class ProviderListResultDto
    {
        public long Id { get; set; }
        public string? Nit { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public IEnumerable<long>? ServiceIds { get; set; }

    }
}
