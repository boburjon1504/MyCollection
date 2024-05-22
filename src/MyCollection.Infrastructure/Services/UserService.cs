using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Repositories.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class UserService(IUserRepository userRepository) : IUserService
{
    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsUniqueEmail(user.Email))
            throw new ArgumentException("Email is already registered");

        if (!IsUniqueUserName(user.UserName))
            throw new ArgumentException("Username is already exist");

        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get()
    {
        return userRepository.Get();
    }

    public async ValueTask<IList<User>> GetAsync(CancellationToken cancellationToken)
    {
        return await userRepository.Get().ToListAsync(cancellationToken);
    }

    public async ValueTask<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await userRepository.Get().FirstOrDefaultAsync(u => u.UserName.Equals(userName));
    }

    public ValueTask<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return userRepository.GetByIdAsync(id, true, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }
    private bool IsUniqueEmail(string email) => !Get().Any(u => u.Email == email);
    private bool IsUniqueUserName(string userName) => !Get().Any(u => u.UserName == userName);

    public ValueTask<bool> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userRepository.DeleteAsync(user, saveChanges, cancellationToken); 
    }
}
