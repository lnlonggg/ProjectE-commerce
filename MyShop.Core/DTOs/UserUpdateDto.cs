using MyShop.Core.Entities;

namespace MyShop.Core.DTOs
{
    public class UserUpdateDto
    {
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }
}