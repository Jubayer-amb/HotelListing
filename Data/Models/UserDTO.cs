using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelListing.Data.Enums;

namespace HotelListing.Data.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [MaxLength(350)]
        public string? Address { get; set; }
        public Role Role { get; set; }
    }
    public class CreateUserDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(350)]
        public string? Email { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string? Password { get; set; }
        [JsonIgnore]
        public Role Role { get; set; } = Role.User;
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [MaxLength(350)]
        public string? Address { get; set; }
        [EmailAddress]
        [MaxLength(350)]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class AuthenticateRequestDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class AuthenticateResponseDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public Role Role { get; set; }
        public string Token { get; set; } = null!;
    }

}