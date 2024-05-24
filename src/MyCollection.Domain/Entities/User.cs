using MyCollection.Domain.Common.Auditables;
using MyCollection.Domain.Enums;
namespace MyCollection.Domain.Entities;
public class User : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public Role Role { get; set; } = Role.User;
    
    public string? ImgPath { get; set; }

    public bool IsActive { get; set; }

    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
