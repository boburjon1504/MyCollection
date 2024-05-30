using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;
using System.Diagnostics;

namespace MyCollection.Web.Controllers;
public class HomeController(IAccountService accountService,
    IUserService userService,
    ICollectionService collectionService,
    ICollectionItemService itemService,
    IContextRequest request) : Controller
{
    public async ValueTask<IActionResult> Index(Pagination pagination)
    {
        var latestItems = await itemService.Get()
            .OrderByDescending(i => i.CreatedDate)
            .Skip(pagination.PageSize * pagination.PageToken)
            .Take(pagination.PageSize)
            .Include(c => c.Owner)
            .Include(c => c.Collection)
            .ToListAsync();
        ViewBag.LatestItems = latestItems;

        var topFiveCollections = await collectionService
            .Get()
            .OrderByDescending(c => c.ItemsCount)
            .Take(5).Include(c => c.Owner)
            .ToListAsync();

        ViewBag.TopFiveCollections = topFiveCollections;

        var user = await request.GetRequestedUserAsync();

        pagination.PageToken++;

        return View(new ModelForView { User = user, Pagination = pagination });
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(User user)
    {
        try
        {
            var token = await accountService.RegisterAsync(user, HttpContext.RequestAborted);

            AppendTokenToCooky(token);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View("Index", new ModelForView { User = user });
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async ValueTask<IActionResult> Login(User user)
    {
        try
        {
            var token = await accountService.LoginAsync(new User { UserName = user.UserName, Password = user.Password });

            AppendTokenToCooky(token);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View("Index", new ModelForView { User = user });
        }
        return RedirectToAction("Index", "Home");
    }

    private void AppendTokenToCooky(string token)
    {
        Response.Cookies.Append("token", token, new CookieOptions { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(1) });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
