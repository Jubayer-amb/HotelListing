using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelListing.Data.Enums;

namespace HotelListing.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    [MaxLength(250)]
    public string? FirstName { get; set; }
    [MaxLength(250)]
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    [EmailAddress]
    [MaxLength(350)]
    public string Email { get; set; } = null!;
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = null!;
    [MaxLength(350)]
    public string? Address { get; set; }
    public Role Role { get; set; }
    [JsonIgnore]
    public string HashedPassword { get; set; } = null!;
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}