using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuthPlatform.Mvc.Models;

namespace AuthPlatform.Mvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


}
