using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Enums;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers;
public class UserManagementController(IUserService userService) : Controller
{
    [Authorize(Roles = "Admin")]
    public async ValueTask<IActionResult> Users()
    {
        var user = await userService.GetByIdAsync(Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value));
        var users = await userService.GetAsync(HttpContext.RequestAborted);
        ViewBag.Users = users;
        var model = new ModelForView
        {
            User = user
        };

        return View(model);
    }
}
