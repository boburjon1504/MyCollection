using MyCollection.Application.Common.Models;
using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface IAccountService
{
    ValueTask<string> RegisterAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<string> LoginAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<VerificationCode> SendVerificationCodeAsync(User code);

    ValueTask<VerificationCode> ResendVerificationCodeAsync(VerificationCode code);
}
