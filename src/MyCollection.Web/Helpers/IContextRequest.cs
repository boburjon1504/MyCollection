using MyCollection.Domain.Entities;

namespace MyCollection.Web.Helpers;

public interface IContextRequest
{
    ValueTask<User?> GetRequestedUserAsync();
}
