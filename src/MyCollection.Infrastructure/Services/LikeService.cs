using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Repositories;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class LikeService(ILikeRepository likeRepository) : ILikeService
{
    public ValueTask<Like> CreateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return likeRepository.CreateAsync(like, saveChanges, cancellationToken);
    }

    public IQueryable<Like> Get()
    {
        return likeRepository.Get();
    }

    public async ValueTask<IList<Like>> GetAsync(CancellationToken cancellationToken)
    {
        return await likeRepository.Get().ToListAsync(cancellationToken);
    }

    public ValueTask<Like?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return likeRepository.GetByIdAsync(id, true, cancellationToken);
    }

    public ValueTask<Like> UpdateAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return likeRepository.UpdateAsync(like, saveChanges, cancellationToken);
    }

    public ValueTask<bool> DeleteAsync(Like like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return likeRepository.DeleteAsync(like, saveChanges, cancellationToken);
    }
}
