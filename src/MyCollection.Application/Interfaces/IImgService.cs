using Microsoft.AspNetCore.Http;

namespace MyCollection.Application.Interfaces;
public interface IImgService
{
    ValueTask<string> SaveImgAsync(IFormFile img, Guid pathHelper);

    ValueTask DeleteAsync(string path);
}
