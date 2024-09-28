using Application.DTOs;

namespace Application.DTOs
{
    public class UserDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
