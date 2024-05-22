using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Web.Models;
namespace MyCollection.Web.Controllers;
[Authorize]
public class AccountController(IUserService userService, IWebHostEnvironment webHost,IImgService imgService) : Controller
{
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");

        return RedirectToAction("Index", "Home");
    }


    public async ValueTask<IActionResult> Profile(string userName)
    {
        var user = await userService.GetByUserNameAsync(userName);

        if (user is null)
            user = await userService.GetByIdAsync(Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value));


        var edit = new UserForEdit
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
        };

        ViewBag.Edit = edit;

        return View(new ModelForView
        {
            User = user,
            ProfileImg = new ProfileImg { UserId = user.Id }
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> UploadImg(ProfileImg profileImg)
    {
        var user = await userService.GetByIdAsync(profileImg.UserId);
        var root = webHost.WebRootPath;
        
        if (profileImg.Img is null)
        {
            return View("Profile", new ModelForView { User = user, ProfileImg = new ProfileImg { UserId = user.Id } });
        }

        if (profileImg.Img.Length > 500000)
        {
            ModelState.AddModelError("", $"Img size is over 500kb, please resize it or choose another img");
            return View("Profile", new ModelForView { User = user, ProfileImg = new ProfileImg { UserId = user.Id } });

        }


        user.ImgPath = await imgService.SaveImgAsync(profileImg.Img, profileImg.UserId, root);

        await userService.UpdateAsync(user, cancellationToken: HttpContext.RequestAborted);

        return RedirectToAction("Profile", "Account", new { userName = user.UserName });
    }

    public async ValueTask<IActionResult> DeletImg(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        if (user.ImgPath is not null)
        {

            await imgService.DeleteAsync(user.ImgPath,webHost.WebRootPath);

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

    public async ValueTask<IActionResult> Delete(Guid user)
    {
        Response.Cookies.Delete("token");

        return RedirectToAction("Index", "Home");
    }
}
