namespace HotelLIsting.Data
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string? Description { get; set; }
        public IList<Hotel>? Hotels { get; set; }

    }
}