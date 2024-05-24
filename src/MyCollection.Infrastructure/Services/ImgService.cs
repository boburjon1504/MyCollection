using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using MyCollection.Application.Interfaces;

namespace MyCollection.Infrastructure.Services;
public class ImgService() : IImgService
{
    public async ValueTask<string> SaveImgAsync(IFormFile img, Guid pathHelper)
    {

        var blobContainer = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=mycollection;AccountKey=BnTlqZ7V8Zge71ULN+sJhuOoKLiuxWFqofIpqPa3qwb362+2py29BCn0hVuppUbgcZGX/5nZDLpT+AStqNidvA==;EndpointSuffix=core.windows.net",
            "mycollectionimg");

        var imgPath = $"{pathHelper}{Path.GetExtension(img.FileName)}";

        var blobClient = blobContainer.GetBlobClient(imgPath);

        var memoryStream = new MemoryStream();


        await img.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        await blobClient.UploadAsync(memoryStream);

        var url = blobClient.Uri.AbsoluteUri;

        return url;

        //var extension = Path.GetExtension(img.FileName);
        //var imgPath = Path.Combine("Images", $"{pathHelper}{extension}");

        //var fullPath = GetFullPath(imgPath, rootPath);

        //if (File.Exists(fullPath))
        //{
        //    File.Delete(fullPath);
        //}

        //var file = File.Open(fullPath,FileMode.OpenOrCreate);

        //await img.CopyToAsync(file);

        //file.Close();

        //return imgPath.Replace(@"\","/");
    }

    public async ValueTask DeleteAsync(string path)
    {
        var blobContainer = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=mycollection;AccountKey=BnTlqZ7V8Zge71ULN+sJhuOoKLiuxWFqofIpqPa3qwb362+2py29BCn0hVuppUbgcZGX/5nZDLpT+AStqNidvA==;EndpointSuffix=core.windows.net",
           "mycollectionimg");

        var blobName = path.Split("/").LastOrDefault();

        var result = await blobContainer.DeleteBlobIfExistsAsync(blobName);

        Console.WriteLine(result);
    }

    private string GetFullPath(string imgPath, string rootPath) => Path.Combine(rootPath, imgPath);
}
