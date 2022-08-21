using HotelLIsting.Data;

namespace HotelLIsting.IRepository;

public interface IUnitOfWork
{
    IGenericRepository<Country> Countries { get; }
    IGenericRepository<Hotel> Hotels { get; }
    Task save();
}