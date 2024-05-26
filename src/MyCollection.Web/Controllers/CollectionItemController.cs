using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers;
public class CollectionItemController(IUserService userService) : Controller
{
    [HttpGet("{userName}/{collectionName}/{itemName}")]
    public async ValueTask<IActionResult> GetCollectionItem(string userName,string collectionName,string itemName)
    {
        var user = await userService.GetByUserNameAsync(userName);

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }
}
