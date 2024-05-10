using MyCollection.Domain.Entities;

namespace MyCollection.Application.Interfaces;
public interface ITokenGeneratorService
{
    string GenerateToken(User user);
}
