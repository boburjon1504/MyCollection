namespace MyCollection.Application.Common.Helpers;
public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password,string passwordHash);
}
