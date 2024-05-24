using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class CollectionItemService(ICollectionItemRepository collectionItemRepository) : ICollectionItemService
{
    public ValueTask<CollectionItem> CreateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return collectionItemRepository.CreateAsync(collectionItem, saveChanges, cancellationToken);
    }

    public IQueryable<CollectionItem> Get()
    {
        return collectionItemRepository.Get();
    }

    public async ValueTask<IList<CollectionItem>> GetAsync(CancellationToken cancellationToken)
    {
        return await collectionItemRepository.Get().ToListAsync(cancellationToken);
    }

    public ValueTask<CollectionItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return collectionItemRepository.GetByIdAsync(id, true, cancellationToken);
    }

    public ValueTask<CollectionItem> UpdateAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return collectionItemRepository.UpdateAsync(collectionItem, saveChanges, cancellationToken);
    }

    public ValueTask<bool> DeleteAsync(CollectionItem collectionItem, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return collectionItemRepository.DeleteAsync(collectionItem, saveChanges, cancellationToken);
    }
}
