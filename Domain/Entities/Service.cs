namespace Domain.Entities
{
    public class Service : BaseEntity
    {
        public string? Name { get; set; }
        public decimal? PriceByHour { get; set; }
        public long? ProviderId { get; set; }
        public virtual Provider? Provider { get; set; }
        public virtual ICollection<Country>? Countries { get; set; } = [];
    }
}
