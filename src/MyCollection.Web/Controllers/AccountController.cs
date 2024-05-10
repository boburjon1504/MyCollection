using Microsoft.AspNetCore.Mvc;
using MyCollection.Domain.Entities;
using MyCollection.Persistence.Brokers.Interfaces;

namespace MyCollection.Web.Controllers;
public class AccountController() : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(User user)
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(User user)
    {
        
        return View();
    }

    private string GetCachKey(string modelName, string registrationId) => string.Join("_", modelName, registrationId);



}
