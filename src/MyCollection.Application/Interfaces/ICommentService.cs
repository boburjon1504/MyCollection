using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface ICommentService
{
    IQueryable<Comment> Get();

    ValueTask<IList<Comment>> GetAsync(CancellationToken cancellationToken);

    ValueTask<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<bool> DeleteAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);
}
