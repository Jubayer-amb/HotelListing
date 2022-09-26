using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.Models;

public class CreateCountryDTO
{

    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    public string? Name { get; set; }
    [Required]
    [StringLength(2, ErrorMessage = "The {0} must be max {1} characters long.")]
    public string? ShortName { get; set; }
    public string? Description { get; set; }
}

public class CountryDTO : CreateCountryDTO
{
    public Guid Id { get; set; }
    public IList<HotelDTO>? Hotels { get; set; }

}
