namespace Application.DTOs
{
    public class SummaryResultDto
    {
        public Dictionary<string, int> ProvidersByCountry { get; set; } = [];
        public Dictionary<string, int> ServicesByCountry { get; set; } = [];
    }

}
