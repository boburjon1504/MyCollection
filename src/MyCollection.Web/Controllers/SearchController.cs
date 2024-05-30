using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers;
public class SearchController(ICollectionService collectionService,
    ICommentService commentService,
    ICollectionItemService collectionItemService,
    IUserService userService,
    IContextRequest request) : Controller
{
    public async ValueTask<IActionResult> Search(string searchText)
    {
        var splitedText = searchText.Trim().Split(' ', '?', ',');
        if (splitedText.Length == 1)
        {
            searchText = splitedText[0];
        }
        else if (splitedText.Length > 1)
        {
            searchText = string.Join(" | ", splitedText);
        }

        var matchingComments = await commentService
            .Get()
            .Where(comment => EF.Functions.ToTsVector("english", comment.Message).Matches(EF.Functions.ToTsQuery("english", searchText)))
            .Include(c=>c.Sender)
            .ToListAsync();

        var matchingUsers = await userService.Get()
            .Where(user => EF.Functions.ToTsVector("english", user.FirstName + " " + user.LastName + " " + user.Email + " " + user.UserName)
            .Matches(EF.Functions.ToTsQuery("english", searchText)))
            .ToListAsync();

        var matchingCollections = await collectionService.Get()
            .Where(collection => EF.Functions.ToTsVector("english", collection.Name + " " + collection.Description).Matches(EF.Functions.ToTsQuery("english", searchText)))
            .Include(c=>c.Owner)
            .ToListAsync();

        var matchingItems = await collectionItemService.Get()
            .Where(item => EF.Functions.ToTsVector("english", item.Name + " " + item.Description).Matches(EF.Functions.ToTsQuery("english", searchText)))
            .Include(c=>c.Owner)
            .Include(i=>i.Collection)
            .ToListAsync();

        var user = await request.GetRequestedUserAsync();


        ViewBag.Users = matchingUsers;
        ViewBag.Collections = matchingCollections;
        ViewBag.Items = matchingItems;
        ViewBag.Comments = matchingComments;

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }
}
