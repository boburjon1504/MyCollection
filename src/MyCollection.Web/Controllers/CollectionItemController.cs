using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Infrastructure.Services;
using MyCollection.Web.Models;
using System.Security.Claims;

namespace MyCollection.Web.Controllers;
public class CollectionItemController(IUserService userService, ICollectionItemService collectionItemService, IImgService imgService) : Controller
{
    [HttpGet]
    public async ValueTask<IActionResult> GetCollectionItem(string userName, string collectionName, string itemName)
    {
        User user;
        if (User.Identity.IsAuthenticated)
        {
            user = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value));
        }
        else
        {
            user = new User();
        }
        var item = await collectionItemService
            .Get()
            .Include(i => i.Collection)
            .Include(i => i.Owner)
            .Include(i => i.Comments)
            .ThenInclude(c => c.Sender)
            .FirstOrDefaultAsync(i => i.Name.Equals(itemName));

        ViewBag.Item = item;

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var items = await collectionItemService.Get().Include(i => i.Owner).Include(i => i.Collection).ToListAsync();
        User user;

        if (User.Identity.IsAuthenticated)
        {
            user = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));
        }
        else
        {
            user = new User();
        }

        ViewBag.Items = items;

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
        collectionItem.CreatedDate = DateTimeOffset.UtcNow;
        var newCollection = await collectionItemService.CreateAsync(collectionItem, saveChanges: true, cancellationToken: HttpContext.RequestAborted);

        User user = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));

        var item = await collectionItemService
            .Get()
            .Include(i => i.Collection)
            .Include(i => i.Owner)
            .FirstOrDefaultAsync(i => i.Id == collectionItem.Id);

        return RedirectToAction("GetCollection", "Collection", new { userName = item.Owner.UserName, collectionName = item.Collection.Name });
    }

}

