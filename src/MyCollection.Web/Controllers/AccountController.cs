using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Web.Models;
namespace MyCollection.Web.Controllers;
[Authorize]
public class AccountController(IUserService userService, IWebHostEnvironment webHost) : Controller
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

        var extension = Path.GetExtension(profileImg.Img.FileName);
        var filePath = Path.Combine("Images", $"{user.Id}{extension}");


        var fullPath = Path.Combine(root, filePath);

        if (user.ImgPath is not null)
        {
            System.IO.File.Delete(fullPath);
        }

        var file = new FileStream(fullPath, FileMode.OpenOrCreate);
        await profileImg.Img.CopyToAsync(file);
        file.Close();

        user.ImgPath = filePath.Replace(@"\", "/");

        await userService.UpdateAsync(user, cancellationToken: HttpContext.RequestAborted);

        return RedirectToAction("Profile", "Account", new { userName = user.UserName });
    }

    public async ValueTask<IActionResult> DeletImg(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        if (user.ImgPath is not null)
        {

            var root = webHost.WebRootPath;

            var filePath = user.ImgPath.Replace("/", @"\");

            var fullPath = Path.Combine(root, filePath);


            System.IO.File.Delete(fullPath);

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
