using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

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
                return new Dictionary<string, string>();

            return JsonSerializer.Deserialize<Dictionary<string, string>>(PersonalizedFieldsJson) ?? new Dictionary<string, string>();
        }
        set
        {
            PersonalizedFieldsJson = JsonSerializer.Serialize(value ?? new Dictionary<string, string>());
        }
    }
}
