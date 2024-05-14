using MyCollection.Domain.Common.Auditables;
using System.ComponentModel.DataAnnotations;
namespace MyCollection.Domain.Entities;
public class User : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string? ImgPath { get; set; }
}
