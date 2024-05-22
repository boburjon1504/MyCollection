using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.DataContext;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Persistence.Repositories;
public class UserRepository(CollectionDbContext dbContext) : EntityBaseRepository<User>(dbContext), IUserRepository
{
    public IQueryable<User> Get(bool asNoTracking = true)
    {
        var initialQuery = DbContext.Users;

        if (asNoTracking)
            initialQuery.AsNoTracking();

        return initialQuery;
    }

    public ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public new ValueTask<User> CreateAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public new ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public async new ValueTask<bool> DeleteAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
        await base.DeleteAsync(user, saveChanges, cancellationToken);

        return true;
    }
}