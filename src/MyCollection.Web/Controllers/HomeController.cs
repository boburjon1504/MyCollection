using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Web.Models;
using System.Diagnostics;

namespace MyCollection.Web.Controllers;
public class HomeController(IAccountService accountService, IUserService userService) : Controller
{
    public async ValueTask<IActionResult> Index()
    {
        var a = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId"));
        if (a is null)
            return View();

         var userId = Guid.Parse(a.Value);
        var user = await userService.GetByIdAsync(userId, HttpContext.RequestAborted);

        return View(new ModelForView { User=user, LoginDetails = new LoginDetails()});
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
            return View("Index",new ModelForView { User=user});
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
            return View("Index",new ModelForView { User=user});
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
