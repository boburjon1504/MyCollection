﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Web.Models;
namespace MyCollection.Web.Controllers;
[Authorize]
public class AccountController(IUserService userService, IWebHostEnvironment webHost, IImgService imgService) : Controller
{

    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async ValueTask<IActionResult> Profile(string userName)
    {
        var requestUser = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));
        var user = await userService
            .Get()
            .Include(c => c.Collections)
            .FirstOrDefaultAsync(u => u.UserName == userName);

        if (user is null)
            user = await userService.GetByIdAsync(Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));

        ViewBag.User = user;

        return View(new ModelForView
        {
            User = requestUser,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> UploadImg(ProfileImg profileImg)
    {
        var requestUser = await userService.GetByIdAsync(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));

        var user = await userService
            .Get()
            .Include(c => c.Collections)
            .FirstOrDefaultAsync(u => u.Id == profileImg.UserId);

        if (profileImg.Img is null)
        {
            return View("Profile", new ModelForView { User = requestUser, ProfileImg = new ProfileImg { UserId = user.Id } });
        }

        if (profileImg.Img.Length > 500000)
        {
            ModelState.AddModelError("", $"Img size is over 500kb, please resize it or choose another img");
            return View("Profile", new ModelForView { User = requestUser, ProfileImg = new ProfileImg { UserId = user.Id } });

        }

        user.ImgPath = await imgService.SaveImgAsync(profileImg.Img, profileImg.UserId);

        await userService.UpdateAsync(user, cancellationToken: HttpContext.RequestAborted);

        return RedirectToAction("Profile", "Account", new { userName = user.UserName });
    }

    public async ValueTask<IActionResult> DeletImg(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        if (user.ImgPath is not null)
        {

            await imgService.DeleteAsync(user.ImgPath);

            user.ImgPath = null;

            await userService.UpdateAsync(user);
        }

        return RedirectToAction("Profile", "Account", new { userName = user.UserName });
    }

    public async ValueTask<IActionResult> Edit(User user)
    {
        var foundUser = await userService.GetByIdAsync(user.Id, HttpContext.RequestAborted);

        var isUnique = !userService.Get().Where(u => u.Id != user.Id).Any(u => u.UserName.Equals(user.UserName));

        if (!isUnique)
        {
            ModelState.AddModelError("", $"Username {user.UserName} is already exist");

            return View("Profile", new ModelForView { User = foundUser, ProfileImg = new ProfileImg { UserId = foundUser.Id } });
        }

        foundUser.FirstName = user.FirstName;
        foundUser.LastName = user.LastName;
        foundUser.UserName = user.UserName;

        await userService.UpdateAsync(foundUser, cancellationToken: HttpContext.RequestAborted);

        return RedirectToAction("Profile", "Account", new { userName = user.UserName });
    }

    public async ValueTask<IActionResult> Delete(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId, HttpContext.RequestAborted);

        await userService.DeleteAsync(user, saveChanges: true, cancellationToken: HttpContext.RequestAborted);

        await imgService.DeleteAsync(user.ImgPath);

        Response.Cookies.Delete("token");

        return RedirectToAction("Index", "Home");
    }
}
