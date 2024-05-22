using Microsoft.AspNetCore.Http;
using MyCollection.Application.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class ImgService() : IImgService
{
    public async ValueTask<string> SaveImgAsync(IFormFile img, Guid pathHelper, string rootPath)
    {
        var extension = Path.GetExtension(img.FileName);
        var imgPath = Path.Combine("/Images", $"{pathHelper}{extension}");

        var fullPath = GetFullPath(rootPath, imgPath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        var file = File.Open(fullPath,FileMode.OpenOrCreate);

        await img.CopyToAsync(file);

        file.Close();

        return imgPath.Replace(@"\","/");
    }

    public async ValueTask DeleteAsync(string path, string rootPath)
    {
        await Task.Run(() =>
        {
            File.Delete(GetFullPath(path, rootPath));
        });
    }

    private string GetFullPath(string rootPath,string imgPath) => Path.Combine(rootPath, imgPath);
}
