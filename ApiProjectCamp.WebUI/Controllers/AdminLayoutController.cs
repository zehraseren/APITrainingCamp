using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.Controllers;

public class AdminLayoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
