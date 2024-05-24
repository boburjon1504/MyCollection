using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface ICollectionService
{
    IQueryable<Collection> Get();

    ValueTask<IList<Collection>> GetAsync(CancellationToken cancellationToken);

    ValueTask<Collection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Collection> CreateAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Collection> UpdateAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<bool> DeleteAsync(Collection collection, bool saveChanges = true, CancellationToken cancellationToken = default);
}
