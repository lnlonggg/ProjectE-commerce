using System;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Core.Entities
{
    public enum Role { Admin, User } // Định nghĩa Role

    public class User
    {
        [Key]
        public Guid Id { get; set; } // Yêu cầu là GUID [cite: 77]

        [Required]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public Role Role { get; set; } = Role.User;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}