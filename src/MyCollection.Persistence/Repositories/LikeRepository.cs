using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.DataContext;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Persistence.Repositories;
public class LikeRepository(CollectionDbContext dbContext) : EntityBaseRepository<Like>(dbContext), ILikeRepository
{
    public IQueryable<Like> Get(bool asNoTracking = true)
    {
        var initialQuery = DbContext.Likes;

        if (asNoTracking)
            initialQuery.AsNoTracking();

        return initialQuery;
    }

    public ValueTask<Like?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public new ValueTask<Like> CreateAsync(Like like, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(like, saveChanges, cancellationToken);
    }

    public new ValueTask<Like> UpdateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(like, saveChanges, cancellationToken);
    }

    public async new ValueTask<bool> DeleteAsync(Like like, bool saveChanges, CancellationToken cancellationToken)
    {
        await base.DeleteAsync(like, saveChanges, cancellationToken);

        return true;
    }

}
