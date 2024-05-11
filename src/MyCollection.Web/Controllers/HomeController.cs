using Microsoft.AspNetCore.Mvc;
using MyCollection.Domain.Entities;
using MyCollection.Web.Models;
using System.Diagnostics;

namespace MyCollection.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        ModelState.AddModelError("", "Ishlamadi");
        return View("Index",user);
    }
    [HttpPost]
    public IActionResult Login(User user)
    {
        return View("Index",new User());
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
