using System.Collections.Generic;

namespace Application.DTOs
{
    public class ProviderDto: BaseEntityDto
    {
        public string? Nit { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public IEnumerable<long>? ServiceIds { get; set; } 
        public Dictionary<string, string>? PersonalizedFields { get; set; } 
    }
}
