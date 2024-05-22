using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.DataContext;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Persistence.Repositories;
public class CollectionItemRepository(CollectionDbContext dbContext):EntityBaseRepository<CollectionItem>(dbContext),ICollectionItemRepository
{
    public IQueryable<CollectionItem> Get(bool asNoTracking = true)
    {
        var initialQuery = DbContext.Items;

        if (asNoTracking)
            initialQuery.AsNoTracking();

        return initialQuery;
    }

    public ValueTask<CollectionItem?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public new ValueTask<CollectionItem> CreateAsync(CollectionItem collectionItem, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(collectionItem, saveChanges, cancellationToken);
    }

    public new ValueTask<CollectionItem> UpdateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(collectionItem, saveChanges, cancellationToken);
    }

    public async new ValueTask<bool> DeleteAsync(CollectionItem collectionItem, bool saveChanges, CancellationToken cancellationToken)
    {
        await base.DeleteAsync(collectionItem, saveChanges, cancellationToken);

        return true;
    }

}
