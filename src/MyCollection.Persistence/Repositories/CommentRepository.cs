using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.DataContext;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Persistence.Repositories;
public class CommentRepository(CollectionDbContext dbContext) : EntityBaseRepository<Comment>(dbContext), ICommentRepository
{
    public IQueryable<Comment> Get(bool asNoTracking = true)
    {
        var initialQuery = DbContext.Comments;

        if (asNoTracking)
            initialQuery.AsNoTracking();

        return initialQuery;
    }

    public ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public new ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(comment, saveChanges, cancellationToken);
    }

    public new ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(comment, saveChanges, cancellationToken);
    }

    public async new ValueTask<bool> DeleteAsync(Comment comment, bool saveChanges, CancellationToken cancellationToken)
    {
        await base.DeleteAsync(comment, saveChanges, cancellationToken);

        return true;
    }

}
