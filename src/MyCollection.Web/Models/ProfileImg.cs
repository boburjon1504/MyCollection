namespace MyCollection.Web.Models;

public class ProfileImg
{
    public Guid UserId { get; set; }

    public IFormFile Img { get; set; }
}
