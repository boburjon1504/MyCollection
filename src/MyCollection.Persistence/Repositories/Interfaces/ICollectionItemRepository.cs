using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.Repositories.Interfaces;
public interface ICollectionItemRepository
{
    IQueryable<CollectionItem> Get(bool asNoTracking = true);

    ValueTask<CollectionItem?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    ValueTask<CollectionItem> CreateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<CollectionItem> UpdateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default);
}
