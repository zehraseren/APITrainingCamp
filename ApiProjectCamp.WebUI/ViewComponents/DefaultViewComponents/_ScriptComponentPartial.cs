using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.ViewComponents.DefaultViewComponents;

public class _ScriptComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
