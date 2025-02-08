using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Abstractions;

namespace PersonDirectory.Infrastructure.Repositories;

public abstract class Repository<T>(ApplicationDbContext dbContext)
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    public async Task<T?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }

    public virtual void Delete(T entity)
    {
        DbContext.Remove(entity);
    }

}
