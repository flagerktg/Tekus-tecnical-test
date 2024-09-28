namespace Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string? Name { get; set; }
        public string? Nit { get; set; }
        public string? Email { get; set; }
        public Dictionary<string, string>? PersonalizedFields { get; set; }
        public virtual ICollection<Service>? Services { get; set; }

    }
}
