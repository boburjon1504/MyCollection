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
    public async ValueTask<IActionResult> GiveRole(Guid userId, Role role)
    {
        var user = await userService.GetByIdAsync(userId);

        user.Role = role;

        await userService.UpdateAsync(user);

        return RedirectToAction("Users");
    }

    public async ValueTask<IActionResult> BlockOrUnblock(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        user.IsActive = !user.IsActive;

        await userService.UpdateAsync(user);

        return RedirectToAction("Users");
    }
    public async ValueTask<IActionResult> Delete(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        await userService.DeleteAsync(user);

        return RedirectToAction("Users");
    }
}
