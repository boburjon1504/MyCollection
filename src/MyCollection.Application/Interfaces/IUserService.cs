using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface IUserService
{
    IQueryable<User> Get();
    ValueTask<IList<User>> GetAsync(CancellationToken cancellationToken);

    ValueTask<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);

    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<bool> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);
}
