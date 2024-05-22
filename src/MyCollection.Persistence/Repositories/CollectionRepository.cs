using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.DataContext;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Persistence.Repositories;
public class CollectionRepository(CollectionDbContext dbContext) : EntityBaseRepository<Collection>(dbContext), ICollectionRepository
{
    public IQueryable<Collection> Get(bool asNoTracking = true)
    {
        var initialQuery = DbContext.Collections;

        if (asNoTracking)
            initialQuery.AsNoTracking();

        return initialQuery;
    }

    public ValueTask<Collection?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public new ValueTask<Collection> CreateAsync(Collection collection, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(collection, saveChanges, cancellationToken);
    }

    public new ValueTask<Collection> UpdateAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(collection, saveChanges, cancellationToken);
    }

    public async new ValueTask<bool> DeleteAsync(Collection collection, bool saveChanges, CancellationToken cancellationToken)
    {
        await base.DeleteAsync(collection, saveChanges, cancellationToken);

        return true;
    }

}
