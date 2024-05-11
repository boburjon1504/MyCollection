using MyCollection.Application.Common.Helpers;
using MyCollection.Application.Common.Models;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Brokers.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class AccountService(
    IUserService userService,
    ICacheBroker cacheBroker,
    IPasswordHasher passwordHasher,
    ITokenGeneratorService tokenGeneratorService
    ) : IAccountService
{
    public async ValueTask<string> RegisterAsync(User user, CancellationToken cancellationToken = default)
    {
        user.Password = passwordHasher.Hash(user.Password);
        var newUser = await userService.CreateAsync(user, true, cancellationToken);

        return tokenGeneratorService.GenerateToken(newUser);
    }

    public async ValueTask<string> LoginAsync(User user, CancellationToken cancellationToken = default)
    {
        var foundUser = await userService.GetByUserNameAsync(user.UserName);

        if (foundUser is null)
            throw new ArgumentException("Username or password is wrong");

        if (!passwordHasher.Verify(user.Password, foundUser.Password))
            throw new ArgumentException("Username or password is wrong");

        return tokenGeneratorService.GenerateToken(foundUser);
    }

    public async ValueTask<VerificationCode> SendVerificationCodeAsync(User user)
    {
        await SetToCacheAsync<User>(user, user.Email);

        var verificationCode = new VerificationCode
        {
            UserEmail = user.Email,
            Code = GetCode()
        };

        await SetToCacheAsync<VerificationCode>(verificationCode, user.Email);

        return verificationCode;
    }

    public async ValueTask<VerificationCode> ResendVerificationCodeAsync(VerificationCode code)
    {
        code.Code = GetCode();

        await SetToCacheAsync<VerificationCode>(code, code.UserEmail);

        return code;
    }

    private async ValueTask SetToCacheAsync<T>(T value, string userEmail)
    {
        var key = GetCachKey(nameof(T), userEmail);

        await cacheBroker.SetAsync<T>(key, value);
    }
    private string GetCachKey(string modelName, string userEmail) => string.Join("_", modelName, userEmail);

    private int GetCode() => new Random().Next(1000, 10000);
}
