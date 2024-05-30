using MyCollection.Domain.Common.Auditables;
using MyCollection.Domain.Enums;
using NpgsqlTypes;
namespace MyCollection.Domain.Entities;
public class User : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public Role Role { get; set; } = Role.User;

    public NpgsqlTsVector SearchVector { get; set; }
    
    public string? ImgPath { get; set; }

    public bool IsActive { get; set; } = true;

    public string Mode { get; set; } = "dark";

    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
