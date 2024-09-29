namespace Application.DTOs
{
    public class ServiceDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public decimal? PriceByHour { get; set; }

    }
}
