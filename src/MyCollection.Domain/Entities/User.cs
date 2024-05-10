using MyCollection.Domain.Auditable;

namespace MyCollection.Domain.Entities;
public class User : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;
}
