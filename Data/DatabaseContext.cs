using HotelListing.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
}
