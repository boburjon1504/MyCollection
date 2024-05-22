using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.Repositories.Interfaces;
public interface ICommentRepository
{
    IQueryable<Comment> Get(bool asNoTracking = true);

    ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);
}
