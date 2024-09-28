using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string? Nit { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public virtual ICollection<Service>? Services { get; set; }
        public string? PersonalizedFieldsJson { get; set; }
        [NotMapped]
        public Dictionary<string, string> PersonalizedFields
        {
            get
            {
                if (string.IsNullOrEmpty(PersonalizedFieldsJson))
                    return [];

                return JsonSerializer.Deserialize<Dictionary<string, string>>(PersonalizedFieldsJson) ?? [];
            }
            set
            {
                PersonalizedFieldsJson = JsonSerializer.Serialize(value ?? []);
            }
        }

    }
}
