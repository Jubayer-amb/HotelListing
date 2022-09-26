using System.Linq.Expressions;
using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DatabaseContext _context;
    protected readonly DbSet<T> _db;
    public GenericRepository(DatabaseContext context)
    {
        _context = context;
        _db = context.Set<T>();
    }
    public async Task<IList<T>> GetAll(
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<string>? includes = null
        )
    {
        var query = _db.AsQueryable();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        if (expression != null)
        {
            query = query.Where(expression);
        }
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.AsNoTracking().ToListAsync();
    }
    public async Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
    {
        var query = _db.AsQueryable();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }
    public async Task<T> Insert(T entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task InsertRange(IEnumerable<T> entities)
    {
        await _db.AddRangeAsync(entities);
    }
    public async Task<T> Update(T entity)
    {
        _db.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await _db.FindAsync(id);
        if (entity != null)
        {
            _db.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }
}