namespace HotelListing.Helper;

public class AppSettings
{
    public const string Encriptions = "Encriptions";
    public string JwtSecret { get; set; } = null!;
    public string TokenExpiration { get; set; } = null!;
}