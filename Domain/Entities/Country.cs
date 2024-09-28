namespace Domain.Entities
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public virtual ICollection<Service>? Services { get; set; }
    }
}
