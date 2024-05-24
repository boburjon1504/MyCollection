using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface ICollectionItemService
{
    IQueryable<CollectionItem> Get();

    ValueTask<IList<CollectionItem>> GetAsync(CancellationToken cancellationToken);

    ValueTask<CollectionItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<CollectionItem> CreateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<CollectionItem> UpdateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<bool> DeleteAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default);
}
