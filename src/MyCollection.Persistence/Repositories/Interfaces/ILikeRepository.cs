using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.Repositories.Interfaces;
public interface ILikeRepository
{
    IQueryable<Like> Get(bool asNoTracking = true);

    ValueTask<Like?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    ValueTask<Like> CreateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Like> UpdateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default);
}
