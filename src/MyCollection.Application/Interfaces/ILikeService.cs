using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface ILikeService
{
    IQueryable<Like> Get();

    ValueTask<IList<Like>> GetAsync(CancellationToken cancellationToken);

    ValueTask<Like?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Like> CreateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Like> UpdateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<bool> DeleteAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default);
}
