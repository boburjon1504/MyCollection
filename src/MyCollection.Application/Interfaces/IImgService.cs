using Microsoft.AspNetCore.Http;

namespace MyCollection.Application.Interfaces;
public interface IImgService
{
    ValueTask<string> SaveImgAsync(IFormFile img, Guid pathHelper, string rootPath);

    ValueTask DeleteAsync(string path, string rootPath);
}
