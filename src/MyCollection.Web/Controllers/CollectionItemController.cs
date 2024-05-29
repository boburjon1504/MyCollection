using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Infrastructure.Services;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers;
public class CollectionItemController(IUserService userService, ICollectionItemService collectionItemService, IImgService imgService) : Controller
{
    [HttpGet]
    public async ValueTask<IActionResult> GetCollectionItem(string userName,string collectionName,string itemName)
    {
        var user = await userService.GetByUserNameAsync(userName);

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var collections = await collectionItemService.GetAsync(HttpContext.RequestAborted);
        User user;

        if (User.Identity.IsAuthenticated)
        {
            user = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));
        }
        else
        {
            user = new User();
        }

        ViewBag.Collections = collections;

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create(CollectionItem collectionItem)
    {
        collectionItem.Id = Guid.NewGuid();

        collectionItem.ImgPath = await imgService.SaveImgAsync(collectionItem.ImgForm, collectionItem.Id);
        var newCollection = await collectionItemService.CreateAsync(collectionItem, saveChanges: true, cancellationToken: HttpContext.RequestAborted);

        User user = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));

        ViewBag.Collections = newCollection;

        return RedirectToAction("Get");
    }

}

