using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.Controllers;
public class AIController : Controller
{
    public IActionResult CreateRecipe()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateRecipeWithOpenAI(string prompt)
    {
        var apiKey = "YOUR_API_KEY_HERE";
        return View();
    }
}
