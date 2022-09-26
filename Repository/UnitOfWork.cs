using HotelListing.Data;
using HotelListing.Data.Entities;
using HotelListing.IRepository;

namespace HotelListing.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    public IGenericRepository<Country> Countries { get; }
    public IGenericRepository<Hotel> Hotels { get; }
    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
        Countries = new GenericRepository<Country>(_context);
        Hotels = new GenericRepository<Hotel>(_context);
    }
    public async Task save()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
