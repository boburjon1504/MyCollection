using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class CommentService(ICommentRepository commentRepository): ICommentService
{
    public ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return commentRepository.CreateAsync(comment, saveChanges, cancellationToken);
    }

    public IQueryable<Comment> Get()
    {
        return commentRepository.Get();
    }

    public async ValueTask<IList<Comment>> GetAsync(CancellationToken cancellationToken)
    {
        return await commentRepository.Get().ToListAsync(cancellationToken);
    }

    public ValueTask<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return commentRepository.GetByIdAsync(id, true, cancellationToken);
    }

    public ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return commentRepository.UpdateAsync(comment, saveChanges, cancellationToken);
    }

    public ValueTask<bool> DeleteAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return commentRepository.DeleteAsync(comment, saveChanges, cancellationToken);
    }
}
