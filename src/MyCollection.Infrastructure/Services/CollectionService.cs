using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class CollectionService(ICollectionRepository collectionRepository) : ICollectionService
{
    public ValueTask<Collection> CreateAsync(Collection like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return collectionRepository.CreateAsync(like, saveChanges, cancellationToken);
    }

    public IQueryable<Collection> Get()
    {
        return collectionRepository.Get();
    }

    public async ValueTask<IList<Collection>> GetAsync(CancellationToken cancellationToken)
    {
        return await collectionRepository.Get().ToListAsync(cancellationToken);
    }

    public ValueTask<Collection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return collectionRepository.GetByIdAsync(id, true, cancellationToken);
    }

    public ValueTask<Collection> UpdateAsync(Collection like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return collectionRepository.UpdateAsync(like, saveChanges, cancellationToken);
    }

    public ValueTask<bool> DeleteAsync(Collection like, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return collectionRepository.DeleteAsync(like, saveChanges, cancellationToken);
    }
}
