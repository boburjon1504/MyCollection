using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers;
public class CollectionController(IUserService userService) : Controller
{
    [HttpGet("{userName}/{collectionName}")]
    public async ValueTask<IActionResult> GetCollection(string userName,string collectionName)
    {
        var user = await userService.GetByUserNameAsync(userName);

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }
}
