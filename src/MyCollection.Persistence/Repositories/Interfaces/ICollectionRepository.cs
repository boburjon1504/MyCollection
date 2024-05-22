using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.Repositories.Interfaces;
public interface ICollectionRepository
{
    IQueryable<Collection> Get(bool asNoTracking = true);

    ValueTask<Collection?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    ValueTask<Collection> CreateAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Collection> UpdateAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default);
}
