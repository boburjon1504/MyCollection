using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Common.Auditables;
using MyCollection.Persistence.DataContext;

namespace MyCollection.Persistence.Repositories;
public abstract class EntityBaseRepository<TEntity>(CollectionDbContext dbContext) where TEntity : Entity
{
    protected CollectionDbContext DbContext => dbContext;
    protected async ValueTask<IList<TEntity>> GetAsync(CancellationToken cancellationToken) => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    protected async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (saveChanges)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }
    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        dbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }
    protected async ValueTask<TEntity> DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        dbContext.Set<TEntity>().Remove(entity);

        if (saveChanges)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }
}