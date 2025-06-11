using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.Controllers;

public class CategoryController : Controller
{
    public IActionResult CategoryList()
    {
        return View();
    }
}
