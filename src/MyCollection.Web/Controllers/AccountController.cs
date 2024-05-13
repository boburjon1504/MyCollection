using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;

namespace MyCollection.Web.Controllers;
public class AccountController(IUserService userService) : Controller
{
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");

        return RedirectToAction("Index", "Home");
    }


    public async ValueTask<IActionResult> Profile(string userName)
    {
        var user = await userService.GetByUserNameAsync(userName);

        if(user is null)
            user = await userService.GetByIdAsync(Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c=>c.Type.Equals("UserId")).Value));
        return View(user);
    }

}
