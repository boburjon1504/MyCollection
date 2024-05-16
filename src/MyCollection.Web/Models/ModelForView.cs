using MyCollection.Domain.Entities;

namespace MyCollection.Web.Models;

public class ModelForView
{
    public User User { get; set; } = new User();

    public LoginDetails LoginDetails = new LoginDetails();

    public ProfileImg ProfileImg { get; set; } = new ProfileImg();

    public ErrorViewModel ErrorViewModel { get; set; }
}
