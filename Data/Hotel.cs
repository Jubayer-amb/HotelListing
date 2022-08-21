using System.ComponentModel.DataAnnotations.Schema;

namespace HotelLIsting.Data
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public double Rating { get; set; }
        public string? Description { get; set; }
        [ForeignKey(nameof(Country))]
        public Guid? CountryId { get; set; }
        public Country Country { get; set; } = null!;
    }
}
