using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.Models;

public class CreateHotelDTO
{

    [Required]
    [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    public string? Name { get; set; }
    [Required]
    [StringLength(250, ErrorMessage = "The {0} must be max {1} characters long.")]
    public string? Address { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public double Rating { get; set; }
    public string? Description { get; set; }
    [Required]
    public Guid CountryId { get; set; }
}

public class HotelDTO : CreateHotelDTO
{
    public Guid Id { get; set; }
    public CountryDTO? Country { get; set; }
}
