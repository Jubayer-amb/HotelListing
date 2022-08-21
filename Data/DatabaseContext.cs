using Microsoft.EntityFrameworkCore;

namespace HotelLIsting.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
    }
}